import { Component, OnInit, ViewChild, EventEmitter, Output, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Offer } from 'src/app/models/offer';
import { Vehicle } from 'src/app/models/vehicle';
import { NzNotificationService } from 'ng-zorro-antd';
import { RepositoryService } from 'src/app/services/repository.service';
import { AuthService } from 'src/app/services/auth.service';
import { isUndefined, isNull } from 'util';
import { noUndefined } from '../../../../node_modules/@angular/compiler/src/util';

@Component({
  selector: 'app-offer-form',
  templateUrl: './offer-form.component.html',
  styleUrls: ['./offer-form.component.css']
})
export class OfferFormComponent implements OnInit {

  form: FormGroup;
  offer: Offer;
  @Output() newOffer = new EventEmitter<any>();
  @Input() vehicle: Vehicle;
  @Input() modal: boolean;
  newVehicle = true;
  src: any;
  deadline;

  constructor(
    private fb: FormBuilder,
    private repo: RepositoryService,
    private auth: AuthService,
    private notifierService: NzNotificationService,
  ) { }

  ngOnInit() {
    this.createForm();
    this.deadline = Date.now();
}

createForm() {
    this.form = this.fb.group({
      price: [null, [Validators.required]]
  });
}

    submit() {
      // tslint:disable-next-line:forin
    for (const i in this.form.controls) {
      this.form.controls[ i ].markAsDirty();
      this.form.controls[ i ].updateValueAndValidity();
    }
      const queryoffer = 'Offers';
      const oferta: Offer = new Offer();
      oferta.price = this.form.value.price;
      if (oferta.price < this.vehicle.price) {
        this.notifierService.error('Oferta', 'La oferta creada debe ser mayor que el monto solicitado');
        return;
      }

      if ( Date.now() - this.deadline >= this.vehicle.time) {
        this.hide();
        this.notifierService.error('Oferta', 'EL tiempo de subasta por esta publicacion ha finalizado');
        return;
      }
      oferta.description = 'lo mismo de siempre';
      oferta.userId = this.auth.getUserId();
      oferta.enabled = null;
      oferta.publicationId = this.vehicle.id;
      this.repo.post(queryoffer, oferta).subscribe(() => {
      this.notifierService.success('Oferta', 'La oferta se ha realizado satisfactoriamente');
      this.hide();
      }, error => this.notifierService.error('Oferta', 'Hubo un error al crear la oferta'));
    }

    public show(): void {
    //    this.offerFormModal.show();
    }

    public getTime(date: number) {
      return this.deadline + date;
    }

    public hide(): void {
      this.newOffer.emit();
     //   this.offerFormModal.hide();
    }
    public noNull(body: any) {
      return !isNull(body);
    }
    public IsUndefined(vehicle: Vehicle) {
      return !isUndefined(vehicle);
    }

    public change(photo: string) {
      this.src = photo;
    }
    public onlyNumbers(control: FormControl): { [key: string]: any } {
      if (control.value === null) {
          return null;
      }

      if (isNaN(control.value)) {
          return { 'onlyNumbers': true };
      }
  }

}

import { Component, OnInit } from '@angular/core';
import { Offer } from '../../models/offer';
import { RepositoryService } from '../../services/repository.service';
import { NzNotificationService } from '../../../../node_modules/ng-zorro-antd';
import { AuthService } from '../../services/auth.service';
import { Pagination } from '../../models/pagination';
import { NullAstVisitor } from '../../../../node_modules/@angular/compiler';
import { Brand } from '../../models/brand';
import { Model } from '../../models/model';
import { Vehicle } from '../../models/vehicle';
import { VehicleFilters } from '../../models/vehicle-filters';
import { Observable } from '../../../../node_modules/rxjs';
import { Year } from '../../models/year';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-offer-list',
  templateUrl: './offer-list.component.html',
  styleUrls: ['./offer-list.component.css']
})
export class OfferListComponent implements OnInit {

  offerId;
  oldPrice;
  isVisible: boolean;
  validateForm: FormGroup;
  offers: Array<Offer> = new Array<Offer>();
  brands: Array<Brand> = new Array<Brand>();
  models: Array<Model> = new Array<Model>();
  years: Array<Year> = new Array<Year>();
  locations: Array<Location> = new Array<Location>();
  publications: Array<Vehicle> = new Array<Vehicle>();
  offerUrl = 'Offers';
  userId: string;
  simple = false;
  sizeChange = true;
  pagination: Pagination = new Pagination();
  constructor(private fb: FormBuilder, private repo: RepositoryService,
    private notification: NzNotificationService, private auth: AuthService) {}

  ngOnInit() {
    this.pagination.currentPage = 1;
    this.pagination.itemPerPage = 5;
    this.userId = this.auth.getUserId();
    this.loadData();
    this.isVisible = false;
    this.validateForm = this.fb.group({
      userName: [null, [Validators.required]],
    });
  }

  CurrentPageDataChange(current: number) {
    this.pagination.currentPage = current;
    this.pagination.itemPerPage = 5;
    this.loadData();
 }

 getColor(enabled?: boolean) {
  if (enabled === true) {
      return 'rgb(29, 75, 202)';
  } if (enabled === false) {
      return 'rgba(230, 16, 16, 0.932)';
  }
  return '';
}

  loadData() {
    console.log(this.userId);
      // this.repo.getOffers(this.pagination.currentPage, this.pagination.itemPerPage, this.userId, 'DISTRIBUTOR')
      // .subscribe(data => {
      //     this.offers = data.result as Array<Offer>;
      //     this.pagination = Object.assign({}, data.pagination);
      //   }, error => {
      //     this.notification.error('Error', 'Hubo un error al cargar los datos');
      //   });
      this.repo.getOffers(this.pagination.currentPage, this.pagination.itemPerPage, this.userId, 'DISTRIBUTOR')
      .subscribe(data => {
       // console.log(data.result);
        this.offers = data.result;
        this.pagination = Object.assign({}, data.pagination);
      });
      // this.repo.get('Publications').subscribe(data => {
      //   console.log('Publication');
      //   console.log(data.result);
      //   this.publications = data.result;
      // });
      this.repo.get('brands').subscribe(data => {
        this.brands = data as Array<Brand>;
      });
      this.repo.get('years').subscribe(data => {
        this.years = data as Array<Year>;
      });
      this.repo.get('models').subscribe(data => {
        this.models = data as Array<Model>;
      });
      this.repo.get('locations').subscribe(data => {
        this.locations = data as Array<Location>;
      });
  }

  onDelete(data: any) {
      this.repo.delete(this.offerUrl + '/' + data.id).subscribe(() => {
        // this.loadData();
      });
      this.loadData();
  }

  showModal(price, offerId) {
    this.offerId = offerId;
    this.oldPrice = price;
    this.isVisible = true;
    this.validateForm.patchValue({
      userName: price
    });
  }

  handleOk(): void {
    console.log('Button ok clicked!');
    if (this.oldPrice >= this.validateForm.get('userName').value) {
      this.notification.error('Oferta', 'La oferta debe ser mayor que la anterior');
    } else {
        this.repo.update(`offers/${this.offerId}`, `{ "id": "${this.offerId}",
        "price": "${this.validateForm.get('userName').value}" }`).subscribe(() => {
        this.loadData();
        this.notification.success('Oferta', 'La oferta se ha editado satisfactoriamente');
      });
    }
    this.isVisible = false;
  }

  handleCancel(): void {
    console.log('Button cancel clicked!');
    this.isVisible = false;
    this.validateForm.reset();
  }

}

import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { UploadFile, NzNotificationComponent, NzNotificationService } from '../../../../node_modules/ng-zorro-antd';
import { Brand } from 'src/app/models/brand';
import { Model } from 'src/app/models/model';
import { Version } from 'src/app/models/version';
import { Transmission } from 'src/app/models/transmission';
import { Year } from 'src/app/models/year';
import { Fuel } from 'src/app/models/fuel';
import { DoorsNumber } from 'src/app/models/doors-number';
import { Pack } from 'src/app/models/pack';
import { Location } from 'src/app/models/location';
import { RepositoryService } from 'src/app/services/repository.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';
import { Vehicle } from 'src/app/models/vehicle';
import { isUndefined } from 'util';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {

  @Output() loadPublication = new EventEmitter<any>();
  brands: Array<Brand> = new Array<Brand>();
  models: Array<Model> = new Array<Model>();
  versions: Array<Version> = new Array<Version>();
  transmissionTypes: Array<Transmission> = new Array<Transmission>();
  years: Array<Year> = new Array<Year>();
  fuels: Array<Fuel> = new Array<Fuel>();
  doorsNumbers: Array<DoorsNumber> = new Array<DoorsNumber>();
  locations: Array<Location> = new Array<Location>();
  private brandsUrl = 'brands';
  private modelsUrl = 'models';
  private fuelUrl = 'fuels';
  private transmissionsUrl = 'transmissions';
  private yearsUrl = 'years';
  private versionUrl = 'versions';
  private doorsNumberUrl = 'doorsnumbers';
  private LocationUrl = 'locations';
  validateForm: FormGroup;
  visible = false;
  file: File;
  file1: File;
  file2: File;
  file3: File;
  file4: File;
  uploadf: UploadFile[] = [];
  private publicationId: any;

  submitForm(): void {
    // tslint:disable-next-line:forin
    for (const i in this.validateForm.controls) {
      this.validateForm.controls[ i ].markAsDirty();
      this.validateForm.controls[ i ].updateValueAndValidity();
    }

    if (isUndefined(this.file) || isUndefined(this.file1) || isUndefined(this.file2) ||
    isUndefined(this.file3) || isUndefined(this.file4)) {
      // tslint:disable-next-line:no-unused-expression
      this.notification.error('Subir Fotos', 'Debe subir todas las fotos');
    }
    const vehicleModel: Vehicle = Object.assign({}, this.validateForm.value);
    vehicleModel.userId = this.auth.getUserId();

    this.repo.post('publications', vehicleModel).subscribe( data => {
      this.publicationId = data.text();
      this.uploadFiles();
      this.close();
      this.loadPublication.emit();
      this.validateForm.reset();
    }, error => {
      this.notification.error('Error de registro.', 'Faltan campos por completar correctamente.');
    });

  }

  constructor(private repo: RepositoryService, private auth: AuthService, private userService: UserService,
              private fb: FormBuilder, private notification: NzNotificationService) { }

  ngOnInit() {
    this.createForm();
    this.loadData();
  }

   createForm() {
    this.validateForm = this.fb.group({
      invoiceNumber: [null, [Validators.required]],
      mileage: [null, [Validators.required]],
      serialNumber: [null, [Validators.required]],
      insideColor: [null, [Validators.required]],
      outsideColor: [null, [Validators.required]],
      price: [null, [Validators.required]],
      equipmentDetails: [null, [Validators.required]],
      brandId: [null, [Validators.required]],
      modelId: [null, [Validators.required]],
      fuelId: [null, [Validators.required]],
      locationId: [null, [Validators.required]],
      transmissionId: [null, Validators.required],
      versionId: [null, Validators.required],
      doorsNumberId: [null, Validators.required],
      yearId: [null, [Validators.required]]
    });
   }

   loadData() {
    this.repo.get(this.brandsUrl).subscribe(brand => { this.brands = brand as Array<Brand>; });
    this.repo.get(this.fuelUrl).subscribe(fuel => { this.fuels = fuel as Array<Fuel>; });
    this.repo.get(this.doorsNumberUrl).subscribe(d => { this.doorsNumbers = d as Array<DoorsNumber>; });
    this.repo.get(this.LocationUrl).subscribe(loc => { this.locations = loc as Array<Location>; });
    this.repo.get(this.transmissionsUrl).subscribe(tr => { this.transmissionTypes = tr as Array<Transmission>; });
    this.repo.get(this.yearsUrl).subscribe(y => { this.years = y as Array<Year>; });
    this.repo.get(this.modelsUrl).subscribe(m => { this.models = m as Array<Model>; });
    this.repo.get(this.versionUrl).subscribe(data => { this.versions = data as Array<Version>; });
    // this.repo.get(this.fuelUrl).subscribe(fuel => { this.fuels = fuel as Array<Fuel>; });
   }

   uploadFile(file: File) { this.file = file; }
   uploadFile1(file: File) { this.file1 = file; }
   uploadFile2(file: File) { this.file2 = file; }
   uploadFile3(file: File) { this.file3 = file; }
   uploadFile4(file: File) { this.file4 = file; }

   uploadFiles() {
    this.userService.postPhoto(`photo/publication/${this.publicationId}`,
    // tslint:disable-next-line:no-shadowed-variable
    this.file).subscribe(data => {});
      this.userService.postPhoto(`photo/publication/${this.publicationId}`,
    // tslint:disable-next-line:no-shadowed-variable
    this.file1).subscribe(data => {});
      this.userService.postPhoto(`photo/publication/${this.publicationId}`,
    // tslint:disable-next-line:no-shadowed-variable
    this.file2).subscribe(data => {});
      this.userService.postPhoto(`photo/publication/${this.publicationId}`,
    // tslint:disable-next-line:no-shadowed-variable
    this.file3).subscribe(data => {});
      this.userService.postPhoto(`photo/publication/${this.publicationId}`,
    // tslint:disable-next-line:no-shadowed-variable
    this.file4).subscribe(data => {});
   }
   formatterDollar = (value: number) => `$ ${value === null ? '' : value}`;
   parserDollar = (value: string) => value.replace('$ ', '');

   open(): void {
     this.visible = true;
   }

   close(): void {
     this.visible = false;
     this.validateForm.reset();
   }
}

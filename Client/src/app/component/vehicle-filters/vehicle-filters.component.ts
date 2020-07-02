import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Brand } from 'src/app/models/brand';
import { Model } from 'src/app/models/model';
import { Year } from 'src/app/models/year';
import { Transmission } from 'src/app/models/transmission';
import { Fuel } from 'src/app/models/fuel';
import { DoorsNumber } from 'src/app/models/doors-number';
import { Pack } from 'src/app/models/pack';
import { Location } from 'src/app/models/location';
import { Version } from 'src/app/models/version';
import { VehicleFilters } from 'src/app/models/vehicle-filters';
import { RepositoryService } from 'src/app/services/repository.service';

@Component({
  selector: 'app-vehicle-filters',
  templateUrl: './vehicle-filters.component.html',
  styleUrls: ['./vehicle-filters.component.css']
})
export class VehicleFiltersComponent implements OnInit {

  hidden = true;
  filterForm: FormGroup;
  brands: Array<Brand>;
  models: Array<Model>;
  versions: Array<Version>;
  transmissionTypes: Array<Transmission>;
  years: Array<Year>;
  fuels: Array<Fuel>;
  doorsNumbers: Array<DoorsNumber>;
  locations: Array<Location>;
  packs: Array<Pack>;
  @Output() filtersChange = new EventEmitter<VehicleFilters>();

  constructor(private formBuilder: FormBuilder,
              private repo: RepositoryService) { }

  ngOnInit() {
    this.createForm();

    this.repo.get('brands').subscribe(data => { this.brands = data as Array<Brand>; });
    this.repo.get('fuels').subscribe(data => { this.fuels = data as Array<Fuel>; });
    this.repo.get('models').subscribe(data => { this.models = data as Array<Model>; });
    this.repo.get('versions').subscribe(data => { this.versions = data as Array<Version>; });
    this.repo.get('transmissions').subscribe(data => { this.transmissionTypes = data as Array<Transmission>; });
    this.repo.get('years').subscribe(data => { this.years = data as Array<Year>; });
    this.repo.get('doorsnumbers').subscribe(data => { this.doorsNumbers = data as Array<DoorsNumber>; });
    this.repo.get('locations').subscribe(data => { this.locations = data as Array<Location>; });
    this.repo.get('packs').subscribe(data => { this.packs = data as Array<Pack>; });
  }

  createForm() {
    this.filterForm = this.formBuilder.group({
        brandId: null,
        modelId: null,
        versionId: null,
        transmissionTypeId: null,
        yearId: null,
        fuelId: null,
        doorsNumberId: null,
        locationId: null,
        packId: null,
        invoiceNumber: null,
        minMileage: null,
        maxMileage: null,
        serialNumber: null,
        outsideColor: null,
        insideColor: null,
        minPrice: null,
        maxPrice: null
    });
  }

    resetForm() {
      this.filterForm.reset();
      this.onFiltersChange();
  }

  onFiltersChange() {
      this.filtersChange.emit(this.filterForm.value);
  }

  toogleHidden() {
      this.hidden = !this.hidden;
  }

}

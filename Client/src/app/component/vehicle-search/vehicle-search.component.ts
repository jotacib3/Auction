import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { VehicleFiltersComponent } from '../vehicle-filters/vehicle-filters.component';
import { Vehicle } from 'src/app/models/vehicle';
import { VehicleFilters } from 'src/app/models/vehicle-filters';
import { NzNotificationService } from 'ng-zorro-antd';
import { RepositoryService } from 'src/app/services/repository.service';
import { Pagination } from 'src/app/models/pagination';
import { OfferFormComponent } from '../offer-form/offer-form.component';

@Component({
  selector: 'app-vehicle-search',
  templateUrl: './vehicle-search.component.html',
  styleUrls: ['./vehicle-search.component.css']
})
export class VehicleSearchComponent implements OnInit {

  @ViewChild(VehicleFiltersComponent) VehicleFiltersComponent: VehicleFiltersComponent;
    vehicle = new Vehicle();
    vehicles: Array<Vehicle>;
    currentRole: string;
    title: string;
    filters: VehicleFilters = new VehicleFilters();
    pagination: Pagination = new Pagination();
    modal = false;
    loading = false;

    constructor(
        private notification: NzNotificationService,
        private repo: RepositoryService,

    ) { }

    ngOnInit() {
        this.pagination.currentPage = 1;
        this.pagination.itemPerPage = 8;
        this.loadVehicles(this.filters);
        this.title = 'AUTOS PUBLICADOS';
    }

    loadVehicles(filters: VehicleFilters) {
        this.loading = true;
        this.repo.getPublicationsFilters(filters, this.pagination.currentPage, this.pagination.itemPerPage)
            .subscribe(data => {
            this.vehicles = data.result;
            this.pagination = Object.assign({}, data.pagination);
      }, error => {
        this.notification.error('Error', 'Hubo un error al cargar los datos');
      });
    }

    CurrentPageIndexChange(current: number) {
      this.pagination.currentPage = current;
      this.loadVehicles(this.filters);
    }

    onFiltersChange(filters): void {
        this.pagination.currentPage = 1;
        this.loadVehicles(filters);
        this.filters = Object.assign({}, filters);
    }

    onNewOfferEvent(): void {
      this.modal = false;
    }

    addOffer(data: Vehicle): void {
      this.vehicle = data;
      this.modal = true;
    }



}

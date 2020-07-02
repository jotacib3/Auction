import { Component, OnInit } from '@angular/core';
import { RepositoryService } from '../../services/repository.service';
import { NzNotificationService } from '../../../../node_modules/ng-zorro-antd';
import { Vehicle } from '../../models/vehicle';
import { Brand } from '../../models/brand';
import { Fuel } from '../../models/fuel';
import { AuthService } from '../../services/auth.service';
import { VehicleFilters } from '../../models/vehicle-filters';
import { Pagination } from '../../models/pagination';
import { Offer } from '../../models/offer';
import { query } from '../../../../node_modules/@angular/core/src/render3/query';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {

  publications: Array<Vehicle> = new Array<Vehicle>();
  private publicationUrl = 'Publications';
  mapOfExpandData: { [key: string]: boolean } = {};
  distributor = false;
  simple = false;
  sizeChange = true;
  pagination: Pagination = new Pagination();
  deadline;
  prueba = 1580000000000;
  // oferta: Offer;
  constructor(private repo: RepositoryService, private notification: NzNotificationService,
              private auth: AuthService ) { }

  ngOnInit() {
    this.pagination.currentPage = 1;
    this.pagination.itemPerPage = 5;
    this.loadData();
    this.deadline = Date.now();
  }

  getTime(date: number) {
    return this.deadline + date;
  }
       CurrentPageDataChange(current: number) {
         this.pagination.currentPage = current;
         this.pagination.itemPerPage = 5;
         this.loadData();
  }

  getOffer(offers: Array<Offer>) {
    const offer: Array<Offer> = new Array<Offer>();
    if (offers.length) {
      offer.push(offers[0]);
    }
    for ( let i = 1; i < offers.length; i++) {
      if (offer[0].price < offers[i].price) {
          offer[0] = offers[i];
      }
    }
    return offer;
  }
   getColor(enabled?: boolean) {
     if (enabled === false) {
         return 'rgb(29, 75, 202)';
     } else {
         return '';
        }

   }

  loadData() {
      this.repo.getPublications(this.auth.getUserId(), this.auth.getRole(), this.pagination.currentPage, this.pagination.itemPerPage)
      .subscribe(data => {
          this.publications = data.result;
          console.log(this.publications);
          this.pagination = Object.assign({}, data.pagination);
        }, error => {
          this.notification.error('Error', 'Hubo un error al cargar los datos');
        });

  }

}

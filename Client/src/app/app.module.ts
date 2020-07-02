import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule, FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { HttpModule } from '@angular/http';

import { NgZorroAntdModule, NZ_I18N, en_US } from 'ng-zorro-antd';


import { AppComponent } from './component/app/app.component';

/** config angular i18n **/
import { registerLocaleData } from '@angular/common';
import en from '@angular/common/locales/en';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { HomeComponent } from './component/home/home.component';
import { LoginComponent } from './component/account/login/login.component';
import { CatalogsComponent } from './component/catalogs/catalogs.component';
import { CatalogListComponent } from './component/catalogs/catalog/catalog-list/catalog-list.component';
import { BrandListComponent } from './component/catalogs/catalog/brand-list/brand-list.component';
import { CityListComponent } from './component/catalogs/catalog/city-list/city-list.component';
import { DoorsNumberListComponent } from './component/catalogs/catalog/doors-number-list/doors-number-list.component';
import { FuelListComponent } from './component/catalogs/catalog/fuel-list/fuel-list.component';
import { LocationListComponent } from './component/catalogs/catalog/location-list/location-list.component';
import { ModelListComponent } from './component/catalogs/catalog/model-list/model-list.component';
import { PackListComponent } from './component/catalogs/catalog/pack-list/pack-list.component';
import { StateListComponent } from './component/catalogs/catalog/state-list/state-list.component';
import { TransmissionListComponent } from './component/catalogs/catalog/transmission-list/transmission-list.component';
import { VersionListComponent } from './component/catalogs/catalog/version-list/version-list.component';
import { YearListComponent } from './component/catalogs/catalog/year-list/year-list.component';
import { CatalogAddComponent } from './component/catalogs/catalog/catalog-add/catalog-add.component';
import { CatalogEditComponent } from './component/catalogs/catalog/catalog-edit/catalog-edit.component';
import { AuthModule } from './auth/auth.module';
import { RegisterComponent } from './component/account/register/register.component';
import { RegisterEmployeeComponent } from './component/account/register-employee/register-employee.component';
import { ForgotPasswordComponent } from './component/account/forgot-password/forgot-password.component';
import { RecoveryPasswordComponent } from './component/account/recovery-password/recovery-password.component';
import { PasswordChangeComponent } from './component/account/password-change/password-change.component';
import { PhotoUploadComponent } from './component/photo-upload/photo-upload.component';
import { LayoutComponent } from './component/layout/layout.component';
import { MenuComponent } from './component/menu/menu.component';
import { VehicleListComponent } from './component/vehicle-list/vehicle-list.component';
import { EmployeeComponent } from './component/employee/employee.component';
import { DistributorComponent } from './component/distributor/distributor.component';
import { DistributorListComponent } from './component/distributor-list/distributor-list.component';
import { AdminAmdgmComponent } from './component/admin-amdgm/admin-amdgm.component';
import { AdminGmComponent } from './component/admin-gm/admin-gm.component';
import { VehicleSearchComponent } from './component/vehicle-search/vehicle-search.component';
import { OfferListComponent } from './component/offer-list/offer-list.component';
import { VehicleFormComponent } from './component/vehicle-form/vehicle-form.component';
import { VehicleFiltersComponent } from './component/vehicle-filters/vehicle-filters.component';
import { Pie3dGraphicComponent } from './component/pie3d-graphic/pie3d-graphic.component';
import { ColumnGraphicComponent } from './component/column-graphic/column-graphic.component';
import { FusionChartsModule } from '../../node_modules/angular-fusioncharts';
import * as FusionCharts from 'fusioncharts';
import * as Charts from 'fusioncharts/fusioncharts.charts';
import * as FusionTheme from 'fusioncharts/themes/fusioncharts.theme.fusion';
import * as TimeSeries from 'fusioncharts/fusioncharts.timeseries';
import { UserDistributorComponent } from './component/user-distributor/user-distributor.component';
import { UserEmployeeComponent } from './component/user-employee/user-employee.component';
import { AltaEmployeeComponent } from './component/alta-employee/alta-employee.component';
import { AltaPublicationComponent } from './component/alta-publication/alta-publication.component';
import { OfferFormComponent } from './component/offer-form/offer-form.component';

registerLocaleData(en);

FusionChartsModule.fcRoot(FusionCharts, Charts, TimeSeries, FusionTheme);

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    LoginComponent,
    CatalogsComponent,
    CatalogListComponent,
    BrandListComponent,
    CityListComponent,
    DoorsNumberListComponent,
    FuelListComponent,
    LocationListComponent,
    ModelListComponent,
    PackListComponent,
    StateListComponent,
    TransmissionListComponent,
    VersionListComponent,
    YearListComponent,
    CatalogAddComponent,
    CatalogEditComponent,
    RegisterComponent,
    RegisterEmployeeComponent,
    ForgotPasswordComponent,
    PasswordChangeComponent,
    RecoveryPasswordComponent,
    PhotoUploadComponent,
    LayoutComponent,
    MenuComponent,
    VehicleListComponent,
    EmployeeComponent,
    DistributorComponent,
    DistributorListComponent,
    AdminAmdgmComponent,
    AdminGmComponent,
    VehicleSearchComponent,
    OfferListComponent,
    PhotoUploadComponent,
    VehicleFormComponent,
    VehicleFiltersComponent,
    Pie3dGraphicComponent,
    ColumnGraphicComponent,
    UserDistributorComponent,
    UserEmployeeComponent,
    AltaEmployeeComponent,
    AltaPublicationComponent,
    OfferFormComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    HttpModule,
    AuthModule,
    /** import ng-zorro-antd root module，you should import NgZorroAntdModule and avoid importing sub modules directly **/
    NgZorroAntdModule,
    RouterModule.forRoot(appRoutes),
    FusionChartsModule
  ],
  providers: [{ provide: NZ_I18N, useValue: en_US }],
  bootstrap: [AppComponent]
})
export class AppModule { }

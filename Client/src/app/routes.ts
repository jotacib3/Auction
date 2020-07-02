import { Routes } from '@angular/router';
import { HomeComponent } from './component/home/home.component';
import { LoginComponent } from './component/account/login/login.component';
import { CatalogsComponent } from './component/catalogs/catalogs.component';
import { CityListComponent } from './component/catalogs/catalog/city-list/city-list.component';
import { StateListComponent } from './component/catalogs/catalog/state-list/state-list.component';
import { BrandListComponent } from './component/catalogs/catalog/brand-list/brand-list.component';
import { ModelListComponent } from './component/catalogs/catalog/model-list/model-list.component';
import { DoorsNumberListComponent } from './component/catalogs/catalog/doors-number-list/doors-number-list.component';
import { FuelListComponent } from './component/catalogs/catalog/fuel-list/fuel-list.component';
import { LocationListComponent } from './component/catalogs/catalog/location-list/location-list.component';
import { PackListComponent } from './component/catalogs/catalog/pack-list/pack-list.component';
import { TransmissionListComponent } from './component/catalogs/catalog/transmission-list/transmission-list.component';
import { VersionListComponent } from './component/catalogs/catalog/version-list/version-list.component';
import { YearListComponent } from './component/catalogs/catalog/year-list/year-list.component';
import { RegisterComponent } from './component/account/register/register.component';
import { RegisterEmployeeComponent } from './component/account/register-employee/register-employee.component';
import { ForgotPasswordComponent } from './component/account/forgot-password/forgot-password.component';
import { PasswordChangeComponent } from './component/account/password-change/password-change.component';
import { RecoveryPasswordComponent } from './component/account/recovery-password/recovery-password.component';
import { LayoutComponent } from './component/layout/layout.component';
import { EmployeeComponent } from './component/employee/employee.component';
import { VehicleListComponent } from './component/vehicle-list/vehicle-list.component';
import { DistributorComponent } from './component/distributor/distributor.component';
import { AdminGmComponent } from './component/admin-gm/admin-gm.component';
import { AdminAmdgmComponent } from './component/admin-amdgm/admin-amdgm.component';
import { EmployeeGuardService } from './security/employee-guard.service';
import { DistributorGuardService } from './security/distributor-guard.service';
import { AdminGmGuardService } from './security/admin-gm-guard.service';
import { AdminAmdgmGuardService } from './security/admin-amdgm-guard.service';
import { AuthGuardService } from './security/auth-guard.service';
import { OfferListComponent } from './component/offer-list/offer-list.component';
import { VehicleSearchComponent } from './component/vehicle-search/vehicle-search.component';
import { ColumnGraphicComponent } from './component/column-graphic/column-graphic.component';
import { Pie3dGraphicComponent } from './component/pie3d-graphic/pie3d-graphic.component';
import { UserEmployeeComponent } from './component/user-employee/user-employee.component';
import { UserDistributorComponent } from './component/user-distributor/user-distributor.component';
import { AltaEmployeeComponent } from './component/alta-employee/alta-employee.component';
import { AltaPublicationComponent } from './component/alta-publication/alta-publication.component';

export const appRoutes: Routes = [
    {path: 'home', component: HomeComponent},
    {path: '', redirectTo: 'home', pathMatch: 'full'},
    { path: 'login', component: LoginComponent },
    { path: 'register-employee', component: RegisterEmployeeComponent },
    { path: 'change-password', component: PasswordChangeComponent, canActivate: [AuthGuardService] },
    { path: 'register', component: RegisterComponent },
    { path: 'forgot-password', component: ForgotPasswordComponent },
    { path: 'user-distributor', component: UserDistributorComponent },
    { path: 'user-employee', component: UserEmployeeComponent },
    { path: 'recovery-password', component: RecoveryPasswordComponent },
    {
        path: 'user', component: LayoutComponent, children: [
            {
                path: 'employee', component: EmployeeComponent, canActivate: [EmployeeGuardService],
                children: [
                    { path: '', redirectTo: 'publication-list', pathMatch: 'full' },
                    { path: 'publication-list', component: VehicleListComponent }
                ]
            },
            {
                path: 'distributor', component: DistributorComponent, canActivate: [DistributorGuardService],
                children: [
                    { path: '', redirectTo: 'publication-search', pathMatch: 'full' },
                    { path: 'publication-search', component: VehicleSearchComponent },
                    { path: 'offer-list', component: OfferListComponent }
                ]
            },
            {
                path: 'admin-gm', component: AdminGmComponent, canActivate: [AdminGmGuardService],
                children: [
                    { path: '', redirectTo: 'publications', pathMatch: 'full' },
                    { path: 'publication-list', component: VehicleListComponent },
                    { path: 'alta-employee', component: AltaEmployeeComponent },
                    { path: 'publications', component: AltaPublicationComponent }
                ]
            },
            {
                path: 'admin-amdgm', component: AdminAmdgmComponent, canActivate: [AdminAmdgmGuardService],
                children: [
                    { path: 'register-distributor', component: RegisterComponent},
                    { path: '', redirectTo: 'catalog-list', pathMatch: 'full' },
                    {
                        path: 'catalog-list', component: CatalogsComponent,
                        children: [
                            { path: '', redirectTo: 'city-list', pathMatch: 'full' },
                            { path: 'city-list', component: CityListComponent },
                            { path: 'state-list', component: StateListComponent },
                            { path: 'brand-list', component: BrandListComponent },
                            { path: 'model-list', component: ModelListComponent },
                            { path: 'doors-number-list', component: DoorsNumberListComponent },
                            { path: 'fuel-list', component: FuelListComponent },
                            { path: 'location-list', component: LocationListComponent },
                            { path: 'pack-list', component: PackListComponent },
                            { path: 'transmission-list', component: TransmissionListComponent },
                            { path: 'version-list', component: VersionListComponent },
                            { path: 'year-list', component: YearListComponent },
                        ]
                    },
                    {path: 'bar_graphic', component: ColumnGraphicComponent },
                    {path: 'pie_graphic', component: Pie3dGraphicComponent },
                    {path: 'user-employee', component: UserEmployeeComponent },
                    {path: 'user-distributor', component: UserDistributorComponent }
                ]
            }
        ]
    },
];

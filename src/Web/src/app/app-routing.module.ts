import { NgModule } from '@angular/core'
import { Routes, RouterModule } from '@angular/router'

import { ListPostCodeComponent } from './components/list-postcode/list-postcode.component'
import { PageNotFoundComponent } from './components/shared/page-not-found/page-not-found.component'

const routes: Routes = [
  //{ path: '**', component: PageNotFoundComponent }
]

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule {

}

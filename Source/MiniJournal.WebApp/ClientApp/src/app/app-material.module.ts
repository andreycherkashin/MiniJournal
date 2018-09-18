import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import {
  MatButtonModule,
  MatListModule,
  MatCardModule,
  MatToolbarModule,
  MatFormFieldModule,
  MatInputModule,
  MatDividerModule,
  MatSidenavModule,
  MatIconModule
} from '@angular/material';

@NgModule({
  imports: [
    CommonModule,

    MatButtonModule,
    MatListModule,
    MatCardModule,
    MatToolbarModule,
    MatFormFieldModule,
    MatInputModule,
    MatSidenavModule,
    MatIconModule
  ],
  exports: [
    MatButtonModule,
    MatListModule,
    MatCardModule,
    MatToolbarModule,
    MatFormFieldModule,
    MatInputModule,
    MatSidenavModule,
    MatIconModule
  ],
  declarations: []
})
export class AppMaterialModule { }

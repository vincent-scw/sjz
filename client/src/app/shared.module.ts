import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxMasonryModule } from 'ngx-masonry';
import { MomentModule } from 'ngx-moment';
import { AvatarModule } from 'ngx-avatar';
import { AngularEditorModule } from '@kolkov/angular-editor';

import { MaterialModule } from './material.module';
import { ControlsModule } from './controls/controls.module';

@NgModule({
  imports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    FormsModule,
    HttpClientModule,
    MaterialModule,
    BrowserAnimationsModule,
    ControlsModule,
    NgxMasonryModule,
    AngularEditorModule,
    MomentModule,
    AvatarModule
  ],
  exports: [
    BrowserModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MaterialModule,
    FormsModule,
    HttpClientModule,
    MaterialModule,
    BrowserAnimationsModule,
    ControlsModule,
    NgxMasonryModule,
    AngularEditorModule,
    MomentModule,
    AvatarModule
  ]
})

export class SharedModule { }

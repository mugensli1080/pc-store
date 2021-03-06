/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { CategoriesFormComponent } from './categories-form.component';

describe('CategoriesFormComponent', () =>
{
    let component: CategoriesFormComponent;
    let fixture: ComponentFixture<CategoriesFormComponent>;

    beforeEach(async(() =>
    {
        TestBed.configureTestingModule({
            declarations: [CategoriesFormComponent]
        })
            .compileComponents();
    }));

    beforeEach(() =>
    {
        fixture = TestBed.createComponent(CategoriesFormComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () =>
    {
        expect(component).toBeTruthy();
    });
});

import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NotificationButtonComponent } from './notification-button.component';

describe('NotificationButtonComponent', () => {
  let component: NotificationButtonComponent;
  let fixture: ComponentFixture<NotificationButtonComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [NotificationButtonComponent]
    });
    fixture = TestBed.createComponent(NotificationButtonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});

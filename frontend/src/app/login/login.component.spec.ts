
import { ComponentFixture, TestBed, fakeAsync, tick, flushMicrotasks } from '@angular/core/testing';
import { LoginComponent } from './login.component';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';

class MockAuthService {
  login() {
    return Promise.resolve(true);
  }
}

class MockRouter {
  navigate(path: string[]) {}
}

describe('LoginComponent', () => {
  let component: LoginComponent;
  let fixture: ComponentFixture<LoginComponent>;
  let authService: AuthService;
  let router: Router;
  let httpMock: HttpTestingController;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LoginComponent, HttpClientTestingModule],
      providers: [
        { provide: AuthService, useClass: MockAuthService },
        { provide: Router, useClass: MockRouter }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(LoginComponent);
    component = fixture.componentInstance;
    authService = TestBed.inject(AuthService);
    router = TestBed.inject(Router);
    httpMock = TestBed.inject(HttpTestingController);
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should login successfully with valid credentials', fakeAsync(() => {
    spyOn(router, 'navigate');
    spyOn(authService, 'login').and.returnValue(Promise.resolve(true));
    component.username = 'admin';
    component.password = 'password';
    component.login();
    // Simulate backend /api/auth/login success
    const req = httpMock.expectOne('/api/auth/login');
    expect(req.request.method).toBe('POST');
    req.flush({ success: true });
    tick();
    flushMicrotasks();
    expect(component.error).toBeNull();
    expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
  }));

  it('should show error message with invalid credentials', fakeAsync(() => {
    spyOn(authService, 'login').and.returnValue(Promise.resolve(false));
    component.username = 'wrong';
    component.password = 'wrong';
    component.login();
    // Simulate backend /api/auth/login 401 error
    const req = httpMock.expectOne('/api/auth/login');
    expect(req.request.method).toBe('POST');
    req.flush({ message: 'Invalid username or password' }, { status: 401, statusText: 'Unauthorized' });
    tick();
    flushMicrotasks();
    expect(component.error).toBe('Invalid username or password');
  }));
});

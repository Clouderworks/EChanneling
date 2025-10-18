
import { AuthService, UserSession } from '../auth.service';
import { of } from 'rxjs';
import { TestBed } from '@angular/core/testing';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

describe('AuthService', () => {
  let service: AuthService;
  let httpClientSpy: jasmine.SpyObj<HttpClient>;
  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(() => {
    httpClientSpy = jasmine.createSpyObj('HttpClient', ['get', 'post']);
    routerSpy = jasmine.createSpyObj('Router', ['navigate']);
    TestBed.configureTestingModule({
      providers: [
        { provide: HttpClient, useValue: httpClientSpy },
        { provide: Router, useValue: routerSpy },
        AuthService
      ]
    });
    service = TestBed.inject(AuthService);
  });

  it('should start unauthenticated', () => {
    expect(service.isAuthenticated()).toBeFalse();
    expect(service.getUser()).toBeNull();
  });

  it('should fetch and store user session', async () => {
    const fakeUser: UserSession = { username: 'admin', roles: ['Admin'] };
  httpClientSpy.get.and.returnValue(of(fakeUser));
    const user = await service.fetchSession();
    expect(user).toEqual(fakeUser);
    expect(service.isAuthenticated()).toBeTrue();
    expect(service.getUser()).toEqual(jasmine.objectContaining({ username: 'admin' }));
    expect(service.hasRole('Admin')).toBeTrue();
    expect(service.hasAnyRole(['Admin', 'Doctor'])).toBeTrue();
    expect(service.hasRole('Doctor')).toBeFalse();
  });

  it('should clear session on logout', () => {
    (service as any).user = { username: 'x', roles: ['Admin'] };
    httpClientSpy.post.and.returnValue(of({}));
    service.logout();
    expect(service.isAuthenticated()).toBeFalse();
    expect(service.getUser()).toBeNull();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/login']);
  });
});

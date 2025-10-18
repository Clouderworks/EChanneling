
import { RoleGuard } from '../role.guard';
import { AuthService, UserSession } from '../auth.service';
import { Router } from '@angular/router';
import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('RoleGuard', () => {
  let auth: AuthService;
  let router: jasmine.SpyObj<Router>;
  let guard: RoleGuard;

  beforeEach(() => {
    router = jasmine.createSpyObj('Router', ['navigate']);
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [
        { provide: Router, useValue: router },
        AuthService,
        RoleGuard
      ]
    });
    auth = TestBed.inject(AuthService);
    guard = TestBed.inject(RoleGuard);
    (auth as any).user = null;
  });

  function makeRoute(data: any): any {
    return { data } as any;
  }

  it('should allow access if no roles required', () => {
    const route = makeRoute({});
    const can = guard.canActivate(route, {} as any);
    expect(can).toBeTrue();
  });

  it('should allow access if user has required role', () => {
    (auth as any).user = { username: 'a', roles: ['Admin'] } as UserSession;
    const route = makeRoute({ roles: ['Admin'] });
    const can = guard.canActivate(route, {} as any);
    expect(can).toBeTrue();
  });

  it('should deny access and redirect if user lacks role', () => {
    (auth as any).user = { username: 'a', roles: ['Doctor'] } as UserSession;
    const route = makeRoute({ roles: ['Admin'] });
    const can = guard.canActivate(route, {} as any);
    expect(router.navigate).toHaveBeenCalledWith(['/dashboard']);
    expect(can).toBeFalse();
  });
});

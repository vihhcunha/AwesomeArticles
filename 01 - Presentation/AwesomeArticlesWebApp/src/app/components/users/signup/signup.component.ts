import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, fromEvent, merge } from 'rxjs';
import { User } from 'src/app/models/user';
import { UserService } from 'src/app/services/user.service';
import { ValidationMessages, GenericFormValidator, DisplayMessage } from 'src/app/utils/generic-form-validation';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html'
})
export class SignupComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements: ElementRef[];

  formObject: FormGroup;
  validationMessages: ValidationMessages;
  genericValidator: GenericFormValidator;
  displayMessage: DisplayMessage = {};

  loading: boolean = false;

  public user: User;

  constructor(private fb: FormBuilder,
    private toastr: ToastrService,
    private userService: UserService,
    private router: Router) {
    this.definirValidacoes();
  }

  ngOnInit(): void {

    this.formObject = this.fb.group({
      email: ['', [Validators.required]],
      name: ['', [Validators.required]],
      password: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required]],
    });
  }

  ngAfterViewInit() {
    let controlBlurs: Observable<any>[] = this.formInputElements
      .map((formControl: ElementRef) => fromEvent(formControl.nativeElement, 'blur'));

    merge(...controlBlurs).subscribe(() => {
      this.buildValidationMessages();
    });
  }

  public buildValidationMessages() {
    this.displayMessage = this.genericValidator.buildMessages(this.formObject);
  }

  public definirValidacoes() {
    this.validationMessages = {
      email: {
        required: "É necessário definir o e-mail!"
      },
      name: {
        required: "É necessário definir o nome!"
      },
      password: {
        required: "É necessário definir a senha!"
      },
      confirmPassword: {
        required: "É necessário confirmar a senha!"
      }
    };

    this.genericValidator = new GenericFormValidator(this.validationMessages);
  }

  public submit() {
    if (this.formObject.valid) {

      this.user = Object.assign({}, this.user, this.formObject.value);
      this.cadastrar();
    }

    Object.keys(this.formObject.controls).forEach(key => {
      this.formObject.controls[key].markAsTouched();
    });
    this.buildValidationMessages();
  }

  cadastrar() {
    this.loading = true;

    this.userService.add(this.user)
      .subscribe(
        json => {
          this.router.navigate(['/login']);
          this.loading = false;
        },
        e => {
          this.toastr.error(e.error.message);
          this.loading = false;
        }
      )
  }

}

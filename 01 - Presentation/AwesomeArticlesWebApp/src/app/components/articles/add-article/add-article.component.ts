import { AfterViewInit, Component, ElementRef, OnInit, ViewChildren } from '@angular/core';
import { FormControlName, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable, fromEvent, merge } from 'rxjs';
import { Article } from 'src/app/models/article';
import { ArticlesService } from 'src/app/services/articles.service';
import { ValidationMessages, GenericFormValidator, DisplayMessage } from 'src/app/utils/generic-form-validation';

@Component({
  selector: 'app-add-article',
  templateUrl: './add-article.component.html'
})
export class AddArticleComponent implements OnInit, AfterViewInit {

  @ViewChildren(FormControlName, { read: ElementRef })
  formInputElements: ElementRef[];

  formObject: FormGroup;
  validationMessages: ValidationMessages;
  genericValidator: GenericFormValidator;
  displayMessage: DisplayMessage = {};

  loading: boolean = false;

  public article: Article;

  constructor(private fb: FormBuilder,
    private toastr: ToastrService,
    private articleService: ArticlesService,
    private router: Router) {
    this.definirValidacoes();
  }

  ngOnInit(): void {

    this.formObject = this.fb.group({
      title: ['', [Validators.required]],
      content: ['', [Validators.required]]
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
      title: {
        required: "É necessário definir o título do artigo!"
      },
      content: {
        required: "É necessário definir o conteúdo!"
      }
    };

    this.genericValidator = new GenericFormValidator(this.validationMessages);
  }

  public submit() {
    if (this.formObject.valid) {

      this.article = Object.assign({}, this.article, this.formObject.value);
      this.addArticle();
    }

    Object.keys(this.formObject.controls).forEach(key => {
      this.formObject.controls[key].markAsTouched();
    });
    this.buildValidationMessages();
  }

  addArticle() {
    this.articleService.add(this.article)
      .subscribe(
        json => {
          this.router.navigate(['/articles']);
        },
        e => {
          this.toastr.error(e.error.message);
        }
      )
  }

}

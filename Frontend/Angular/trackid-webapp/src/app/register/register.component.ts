import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  registerForm!: FormGroup;
  submitted: boolean = false;
  success: boolean = false;

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]
    });
  }

  private comparePasswords(): boolean {
    return this.registerForm.controls['password'].value === this.registerForm.controls['confirmPassword'].value;
  }

  onSubmit() {
    console.log('submitted');
    this.submitted = true;
    if (this.registerForm.invalid) {
      this.success =false;
      return;
    }

    if(!this.comparePasswords()) {
      this.registerForm.setErrors({ confirmPassword: true });
      this.success = false;
      return;
    }

    this.success = true;
  }

}

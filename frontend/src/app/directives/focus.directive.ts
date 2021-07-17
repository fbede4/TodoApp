import { Directive, ElementRef, AfterContentInit } from '@angular/core';

@Directive({
  selector: '[appFocus]',
})
export class FocusDirective implements AfterContentInit {
  constructor(private el: ElementRef) {}

  ngAfterContentInit(): void {
    this.el.nativeElement.focus();
  }
}

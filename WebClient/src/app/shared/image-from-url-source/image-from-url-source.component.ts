import { HttpClient } from '@angular/common/http';
import { Component, inject, input } from '@angular/core';

@Component({
  selector: 'app-image-from-url-source',
  templateUrl: './image-from-url-source.component.html',
  styleUrl: './image-from-url-source.component.css'
})
export class ImageFromUrlSourceComponent {

  private httpClient = inject(HttpClient);

  private missingImage: string = "/assets/images/missing-image.jpg";

  checkUrlResponse = input<boolean>(true);

  imageUrl = input.required<string>();
  imageAlt = input<string>("");

  classesToApply = input<string>("");

  cssToApply = input<string>("");


  displayImage: string = "";

  ngOnInit(){
    this.setDisplayImage();
    this.checkUrlValidity();
  }

  ngOnChanges(){
    this.setDisplayImage();
    this.checkUrlValidity();
  }

  setDisplayImage(){
    if(!this.imageUrl() || this.imageUrl() == ""){
      this.displayImage = this.missingImage;
    }
    else {
      this.displayImage = this.imageUrl();
    }
  }

  private checkUrlValidity(): void {
    if(this.checkUrlResponse()){
      this.httpClient.get(this.displayImage, { observe: 'response' }).subscribe({
        error: (err) => {
          if(err.status >= 400){
            this.displayImage = this.missingImage;
          }
        }
      })
    }
  }
}

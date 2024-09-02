import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RecipesPageComponent } from './pages/pages/recipes-page/recipes-page.component';
import { RecipeDetailsPageComponent } from './pages/pages/recipe-details-page/recipe-details-page.component';
import { RecipePreviewComponentComponent } from './pages/page-parts/recipe-preview-component/recipe-preview-component.component';
import { RecipeFormComponentComponent } from './pages/page-parts/recipe-form-component/recipe-form-component.component';
import { IngredientsPageComponent } from './pages/pages/ingredients-page/ingredients-page.component';
import { IngredientPreviewComponentComponent } from './pages/page-parts/ingredient-preview-component/ingredient-preview-component.component';
import { provideHttpClient } from '@angular/common/http';
import { LoadingScreenComponentComponent } from './shared/loading-screen-component/loading-screen-component.component';
import { ErrorScreenComponentComponent } from './shared/error-screen-component/error-screen-component.component';
import { SimpleModalComponentComponent } from './shared/simple-modal-component/simple-modal-component.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActionsShortcutsComponentComponent } from './pages/page-parts/actions-shortcuts-component/actions-shortcuts-component.component';
import { IngredientFormComponentComponent } from './pages/page-parts/ingredient-form-component/ingredient-form-component.component';
import { ConfirmationModalComponentComponent } from './shared/confirmation-modal-component/confirmation-modal-component.component';
import { RecipeIngredientFormComponentComponent } from './pages/page-parts/recipe-ingredient-form-component/recipe-ingredient-form-component.component';
import { RecipeIngredientPreviewComponentComponent } from './pages/page-parts/recipe-ingredient-preview-component/recipe-ingredient-preview-component.component';
import { AuthenticationPageComponent } from './pages/pages/authentication-page/authentication-page.component';
import { AppConfigService } from './services/app-config.service';
import { SetRowSizeButtonsComponent } from './shared/set-row-size-buttons/set-row-size-buttons.component';
import { ImageFromUrlSourceComponent } from './shared/image-from-url-source/image-from-url-source.component';
import { ExternalLinkComponent } from './shared/external-link/external-link.component';
import { DataFormComponent } from './shared/data-form/data-form.component';
import { SearchBarComponent } from './shared/search-bar/search-bar.component';

@NgModule({
  declarations: [
    AppComponent,
    RecipesPageComponent,
    RecipeDetailsPageComponent,
    RecipePreviewComponentComponent,
    RecipeFormComponentComponent,
    IngredientsPageComponent,
    IngredientPreviewComponentComponent,
    LoadingScreenComponentComponent,
    ErrorScreenComponentComponent,
    SimpleModalComponentComponent,
    ActionsShortcutsComponentComponent,
    IngredientFormComponentComponent,
    ConfirmationModalComponentComponent,
    RecipeIngredientFormComponentComponent,
    RecipeIngredientPreviewComponentComponent,
    AuthenticationPageComponent,
    SetRowSizeButtonsComponent,
    ImageFromUrlSourceComponent,
    ExternalLinkComponent,
    DataFormComponent,
    SearchBarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [
    provideHttpClient(),
    {
      provide: APP_INITIALIZER,
      useFactory: appInitializerFactory,
      deps: [AppConfigService],
      multi: true,
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

export function appInitializerFactory(
  appConfigService: AppConfigService
) {
  return () => appConfigService.loadConfig();
}
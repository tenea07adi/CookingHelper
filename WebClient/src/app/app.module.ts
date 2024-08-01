import { NgModule } from '@angular/core';
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
import { FormsModule } from '@angular/forms';
import { ActionsShortcutsComponentComponent } from './pages/page-parts/actions-shortcuts-component/actions-shortcuts-component.component';
import { IngredientFormComponentComponent } from './pages/page-parts/ingredient-form-component/ingredient-form-component.component';
import { ConfirmationModalComponentComponent } from './shared/confirmation-modal-component/confirmation-modal-component.component';
import { RecipeIngredientFormComponentComponent } from './pages/page-parts/recipe-ingredient-form-component/recipe-ingredient-form-component.component';
import { RecipeIngredientPreviewComponentComponent } from './pages/page-parts/recipe-ingredient-preview-component/recipe-ingredient-preview-component.component';

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
    RecipeIngredientPreviewComponentComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [provideHttpClient()],
  bootstrap: [AppComponent]
})
export class AppModule { }

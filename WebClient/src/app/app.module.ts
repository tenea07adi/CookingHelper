import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RecipesPageComponent } from './pages/pages/recipes-page/recipes-page.component';
import { RecipeDetailsPageComponent } from './pages/pages/recipe-details-page/recipe-details-page.component';
import { RecipePreviewComponent } from './pages/page-parts/recipe-preview/recipe-preview.component';
import { RecipeFormComponent } from './pages/page-parts/recipe-form/recipe-form.component';
import { IngredientsPageComponent } from './pages/pages/ingredients-page/ingredients-page.component';
import { IngredientPreviewComponent } from './pages/page-parts/ingredient-preview/ingredient-preview.component';
import { provideHttpClient } from '@angular/common/http';
import { LoadingScreenComponent } from './shared/loading-screen/loading-screen.component';
import { ErrorScreenComponent } from './shared/error-screen/error-screen.component';
import { SimpleModalComponent } from './shared/simple-modal/simple-modal.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ActionsShortcutsComponent } from './pages/page-parts/actions-shortcuts/actions-shortcuts.component';
import { IngredientFormComponent } from './pages/page-parts/ingredient-form/ingredient-form.component';
import { ConfirmationModalComponent } from './shared/confirmation-modal/confirmation-modal.component';
import { RecipeIngredientFormComponent } from './pages/page-parts/recipe-ingredient-form/recipe-ingredient-form.component';
import { RecipeIngredientPreviewComponent } from './pages/page-parts/recipe-ingredient-preview/recipe-ingredient-preview.component';
import { AuthenticationPageComponent } from './pages/pages/authentication-page/authentication-page.component';
import { AppConfigService } from './services/app-config.service';
import { SetRowSizeButtonsComponent } from './shared/set-row-size-buttons/set-row-size-buttons.component';
import { ImageFromUrlSourceComponent } from './shared/image-from-url-source/image-from-url-source.component';
import { ExternalLinkComponent } from './shared/external-link/external-link.component';
import { DataFormComponent } from './shared/data-form/data-form.component';
import { SearchBarComponent } from './shared/search-bar/search-bar.component';
import { UiLongPressDirective } from './directives/ui-long-press.directive';
import { IngredientsListExportModalComponent } from './pages/page-parts/ingredients-list-export-modal/ingredients-list-export-modal.component';
import { SelectedRecipesActionsComponent } from './pages/page-parts/selected-recipes-actions/selected-recipes-actions.component';

@NgModule({
  declarations: [
    AppComponent,
    RecipesPageComponent,
    RecipeDetailsPageComponent,
    RecipePreviewComponent,
    RecipeFormComponent,
    IngredientsPageComponent,
    IngredientPreviewComponent,
    LoadingScreenComponent,
    ErrorScreenComponent,
    SimpleModalComponent,
    ActionsShortcutsComponent,
    IngredientFormComponent,
    ConfirmationModalComponent,
    RecipeIngredientFormComponent,
    RecipeIngredientPreviewComponent,
    AuthenticationPageComponent,
    SetRowSizeButtonsComponent,
    ImageFromUrlSourceComponent,
    ExternalLinkComponent,
    DataFormComponent,
    SearchBarComponent,
    UiLongPressDirective,
    IngredientsListExportModalComponent,
    SelectedRecipesActionsComponent
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
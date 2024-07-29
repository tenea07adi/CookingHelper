import { NgModule } from '@angular/core';
import { provideRouter, RouterModule, Routes, withComponentInputBinding } from '@angular/router';
import { RecipesPageComponent } from './pages/pages/recipes-page/recipes-page.component';
import { RecipeDetailsPageComponent } from './pages/pages/recipe-details-page/recipe-details-page.component';
import { IngredientsPageComponent } from './pages/pages/ingredients-page/ingredients-page.component';

const routes: Routes = [
  { path: '', component: RecipesPageComponent },
  { path: 'recipes', component: RecipesPageComponent },
  { path: 'recipe-details/:recipeId', component: RecipeDetailsPageComponent },
  { path: 'ingredients', component: IngredientsPageComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  providers: [provideRouter(routes, withComponentInputBinding())],
  exports: [RouterModule]
})
export class AppRoutingModule { }

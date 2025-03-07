import { NgModule } from '@angular/core';
import { provideRouter, RouterModule, Routes, withComponentInputBinding } from '@angular/router';
import { RecipesPageComponent } from './pages/pages/recipes-page/recipes-page.component';
import { RecipeDetailsPageComponent } from './pages/pages/recipe-details-page/recipe-details-page.component';
import { IngredientsPageComponent } from './pages/pages/ingredients-page/ingredients-page.component';
import { AuthenticationPageComponent } from './pages/pages/authentication-page/authentication-page.component';
import { AuthGuard } from './guards/auth.guard';
import { GroceryListsPageComponent } from './pages/pages/grocery-lists-page/grocery-lists-page.component';
import { GroceryListDetailsPageComponent } from './pages/pages/grocery-list-details-page/grocery-list-details-page.component';

const routes: Routes = [
  { path: '', component: RecipesPageComponent, canActivate: [AuthGuard] },
  { path: 'auth', component: AuthenticationPageComponent },
  { path: 'recipes', component: RecipesPageComponent, canActivate: [AuthGuard] },
  { path: 'recipe-details/:recipeId', component: RecipeDetailsPageComponent, canActivate: [AuthGuard] },
  { path: 'ingredients', component: IngredientsPageComponent, canActivate: [AuthGuard] },
  { path: 'grocery-lists', component: GroceryListsPageComponent, canActivate: [AuthGuard] },
  { path: 'grocery-list-details/:groceryListId', component: GroceryListDetailsPageComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { useHash: true })],
  providers: [provideRouter(routes, withComponentInputBinding())],
  exports: [RouterModule]
})
export class AppRoutingModule { }

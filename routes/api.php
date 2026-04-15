<?php

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Route;

Route::get('/user', function (Request $request) {
    return Response($request->user(),200); //$request->user();
})->middleware('auth:sanctum');
//osszes zene get, oraarend get, felhasznalo ad torol modosit login logout, ...//zene modosit ha kell asztali
//mi lenne ha nem sanctumot hasznalnank hanem csak a user model vagy controllerbe lenne egy useradatoktokenbol ami a where(token,parameter) response
Route::get('/teszttt', function () {return response("oke",204);});
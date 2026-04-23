<?php

namespace App\Http\Controllers;

use App\Models\zenemodel;
use Illuminate\Http\Request;

class zenekontroler extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function lekerosszes()
    {
        //
      //  $token=request()->header("token");
      //  return response()->json( zenemodel::all(),200,["Content-Type"=>"application/json"]);//,JSON_UNESCAPED_UNICODE);
        
    }
    public function lekerosszeszene(){
      //  if (request()->ajax()) {
            //return view("");
            return response()->json(zenemodel::all(),200, ["Content-type"=> "application/json"]);
     //   }
     //   else{
            return response()->json("ajaxxal kapcsolatos problema",403, ["Content-type"=> "application/json"]);
     //   }
    }
    public function zenetorlesidalapjan(Request $r, $id){
    $token = $r->header("token");
    $felhasznalo= app(felhasznalokontroller::class)->tokenheztartozofelhasznalo($token);//app('App\Http\Controllers\felhasznalokontroller')->tokenheztartozofelhasznalo($token);
$zene = zenemodel::find($id);
if(!$token){return response("nincs token megadva",404);}
if(!$felhasznalo){
    return response("rossz token",404);
}
if($felhasznalo->jog<4){
    return response("nincsen joga a zene torleshez",403);
}
if(!$zene) {
    return response("rossz zeneid",404);
}
$zene->delete();
return response("zene sikeresen torolve",200);

}
    //if($token && $felhasznalo->jog>3){
    
    //}

    /**
     * Store a newly created resource in storage.
     */
    public function store(Request $request)
    {
        //
    }

    /**
     * Display the specified resource.
     */
    public function show(zenemodel $zenemodel)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, zenemodel $zenemodel)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(zenemodel $zenemodel)
    {
        //
    }
}

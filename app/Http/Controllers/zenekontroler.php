<?php

namespace App\Http\Controllers;

use App\Models\zenemodel;
use DB;
//use Illuminate\Support\Facades\Facade\DB;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;
use const Dom\VALIDATION_ERR;

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
public function zeneutvonalfrissitesidalapjan(Request $r, $id){
    $token = $r->header("token");
    if(!$token){   return response()->json("nincs token megadva",404,["Content-Type"=> "application/json"]);}
    $felhasznalo= app(felhasznalokontroller::class)->tokenheztartozofelhasznalo($token);//app('App\Http\Controllers\felhasznalokontroller')->tokenheztartozofelhasznalo($token);
if(!$felhasznalo){ return response()->json("rossz token",404,["Content-Type"=> "application/json"]);}
if($felhasznalo->jog< 4){ return response()->json("nincs joga hozza",403,["Content-Type"=>"application/json"]);}
$zene = zenemodel::find($id);
if(!$zene) { return response()->json("nincs ilyen zene",404,["Content-Type"=>"application/json"]); }
if(!$r->has("zeneurl")){return response()->json("nics zeneurl megadva",404,["Content-Type"=> "application/json"]);}
if( !preg_match( '@((^$)|(^[C-Z]{1}:[\\]{1}.+[\\]{1}.*[\\]{1}.+[.]{1}mp3$)|(^zenek[\\]{1}.+[.]{1}mp3$))@',$r->input("zeneurl")) && false){return response("rossz zeneurl megadva",400);}
$zene->update(["zeneurl"=>$r->zeneurl]);
return response()->json("",204,["Content-Type"=> "application/json"]);
}
public function zeneutvonalfrissitesurlalpjan(Request $r, $url){
    $token = $r->header("token");
    if(!$token){   return response()->json("nincs token megadva",404,["Content-Type"=> "application/json"]);}
    $felhasznalo= app(felhasznalokontroller::class)->tokenheztartozofelhasznalo($token);//app('App\Http\Controllers\felhasznalokontroller')->tokenheztartozofelhasznalo($token);
if(!$felhasznalo){ return response()->json("rossz token",404,["Content-Type"=> "application/json"]);}
if($felhasznalo->jog< 4){ return response()->json("nincs joga hozza",403,["Content-Type"=>"application/json"]);}
$zene = zenemodel::where("keresurl",$url)->first();
if(!$zene) { return response()->json("nincs ilyen zene",404,["Content-Type"=>"application/json"]); }
if(!$r->has("zeneurl")){return response()->json("nics zeneurl megadva",404,["Content-Type"=> "application/json"]);}
$zene->update(["zeneurl"=>$r->zeneurl]);
return response()->json("",204,["Content-Type"=> "application/json"]);
}
public function zenefrissites(Request $r){
    $token = $r->header("token");
    if(!$token){   return response()->json("nincs token megadva",404,["Content-Type"=> "application/json"]);}
    $felhasznalo= app(felhasznalokontroller::class)->tokenheztartozofelhasznalo($token);//app('App\Http\Controllers\felhasznalokontroller')->tokenheztartozofelhasznalo($token);
if(!$felhasznalo){ return response()->json("rossz token",404,["Content-Type"=> "application/json"]);}
if($felhasznalo->jog< 4){ return response()->json("nincs joga hozza",403,["Content-Type"=>"application/json"]);}
//$validalt= Validator::make($r->all(),[['keresurl'=>['required_without_all:id|regex:@(^((http)|(https)){1}[:]{1}[\/]{2}.+[\/]{1}.+$)|(^$)|(^[C-Z]{1}:[\\]{1}.+[\\]{1}.*[\\]{1}.+[.]{1}mp3$)|(^zenek{1}[\\]{1}.+[.]{1}mp3$)@']],[['id'=>'required_without_all:keresurl|numeric|min:1']],[['zeneurl'=>'sometimes|regex:@((^$)|(^[C-Z]{1}:[\\]{1}.+[\\]{1}.*[\\]{1}.+[.]{1}mp3$)|(^zenek{1}[\\]{1}.+[.]{1}mp3$))@']], ['eloado'=>['sometimes']],['cim'=>['sometimes']],['lejatszhatoe'=>['sometimes|regex:/^[0-1]{1}$/']],['hossz'=>['sometimes|numeric']],['tema'=>['sometimes']]]);
//if($validalt->fails()){return response()->json("rossz adatok megadva",403,["Content-Type"=>"application/json"]);}
//$zene;
if($r->has('id')){ $zene=zenemodel::find($r->input('id'));}
else{$zene=$zene = zenemodel::where("keresurl",$r->input('keresurl'))->first();}

if(!$zene) { return response()->json("nincs ilyen zene",404,["Content-Type"=>"application/json"]); }
//if(!$r->has("zeneurl")){return response()->json("nics zeneurl megadva",404,["Content-Type"=> "application/json"]);}

//$zene->fill($r->only(['zeneurl','cim','hossz','eloado','cim','lejatszhatoe','tema']));//ha nincs megadva akkor lehet hogy ez nem a legjobb
//$zene->update();
try{
$zene->update($r->only(['zeneurl','cim','hossz','eloado','lejatszhatoe','tema']));
//$zene->update(["zeneurl"=>$r->zeneurl]);
return response()->json("",204,["Content-Type"=> "application/json"]);
}
catch (\Exception $e){response("nem lehet",500);}
}
public function zenetorles(Request $request){
    $token = $request->header( "token");
    if(empty($token)){ return response("nincs token megadva",404);}
     $felhasznalo= app(felhasznalokontroller::class)->tokenheztartozofelhasznalo($token);
if(!$felhasznalo){ return response("rossz token",404); }
if($felhasznalo->jog<4){ return response("nincs joga hozza",403); }
//$validalt=$request->validate([]);
$validalt = Validator::make($request->all(), ['id'=>'required_without_all:keresurl,zeneurl|numeric|min:1',['keresurl'=>['required_without_all:id,zeneurl|regex:@(^((http)|(https)){1}[:]{1}[\/]{2}.+[\/]{1}.+$)|(^$)|(^[C-Z]{1}:[\\]{1}.+[\\]{1}.*[\\]{1}.+[.]{1}mp3$)|(^zenek{1}[\\]{1}.+[.]{1}mp3$)@']],['zeneurl'=>['required_without_all:keresurl,id|regex:@(^[C-Z]{1}:[\\]{1}.+[\\]{1}.*[\\]{1}.+[.]{1}mp3$)|(^zenek{1}[\\]{1}.+[.]{1}mp3$)@']]]);
if($validalt->fails()){return response("rossz adatok megadva",403);}
if($request->has("id")){ $zene=zenemodel::find( $request->input('id'));}
else if($request->has('zeneurl')){$zene=zenemodel::where('zeneurl',$request->input('zeneurl'));}


else{     zenemodel::where('keresurl',$request->input('keresurl'))->first();}
if(empty($zene)){ return response('rossz adatok megadva',403); }

$zene->delete();
return response('',204) ;
}
public function feltoltendok(Request $request){
    return response()->json((DB::select('select * from feltoltendok')),200,["Content-Type"=>"application/json"]);
}
public function lejatszhatozenek(Request $request){
    return response()->json(DB::select("select * from lejatszhatozenek"),200,["Content-Type"=>"application/json"]);
}
public function zenefeltoltes(Request $request){
  $token = $request->header( "token");
    if(empty($token)){ return response("nincs token megadva",404);}
     $felhasznalo= app(felhasznalokontroller::class)->tokenheztartozofelhasznalo($token);
if(!$felhasznalo){ return response("rossz token",404); }
if($felhasznalo->jog<4){ return response("nincs joga hozza",403); }
//$validalt=$request->validate([]);
/*
  'keresurl'=>
['sometimes',
'regex:@(^((http)|(https)){1}[:]{1}[\/]{2}.+[\/]{1}.+$)|(^$)|(^[C-Z]{1}:[\\]{1}.+[\\]{1}.*[\\]{1}.+[.]{1}mp3$)|(^zenek{1}[\\]{1}.+[.]{1}mp3$)@'],
'zeneurl'=>['sometimes',
'regex:@(^[C-Z]{1}:[\\]{1}.+[\\]{1}.*[\\]{1}.+[.]{1}mp3$)|(^zenek{1}[\\]{1}.+[.]{1}mp3$)@'],

 */
$validalt = Validator::make($request->all(), [
    
'keresurl'=>
['sometimes',
'regex:@(^((http)|(https)){1}[:]{1}[\/]{2}.+[\/]{1}.+$)|(^$)|(^[C-Z]{1}:[\\]{1}.+[\\]{1}.*[\\]{1}.+[.]{1}mp3$)|(^zenek{1}[\\]{1}.+[.]{1}mp3$)@'],
'zeneurl'=>['required',
'regex:@(^[C-Z]{1}:[\\]{1}.+[\\]{1}.*[\\]{1}.+[.]{1}mp3$)|(^zenek{1}[\\]{1}.+[.]{1}mp3$)@'],


'cim'=>'sometimes|nullable',
'eloado'=>'sometimes|nullable',
'lejatszatoe'=>'sometimes|nullable|numeric|min:0|max:1',
'hossz'=>'sometimes|nullable|numeric|min:1',
'tema'=>'sometimes|nullable']);
if((!$request->exists("zeneurl"))){ return response($request->all(),400);}
if(($request->has("keresurl") xor
preg_match('@(^((http)|(https)){1}[:]{1}[\/]{2}.+[\/]{1}.+$)|(^[C-Z]{1}:[\\]{1}.+[\\]{1}.*[\\]{1}.+[.]{1}mp3$)|(^zenek{1}[\\]{1}.+[.]{1}mp3$)@',
$request->input("keresurl")) )&&
 preg_match('@(^[C-Z]{1}:[\\]{1}.+[\\]{1}.*[\\]{1}.+[.]{1}mp3$)|(^zenek{1}[\\]{1}.+[.]{1}mp3$)@', $request->input("zeneurl")))
 {return response("rossz adatok megadva",400);}
//if($validalt->fails()){return response($validalt->errors()); }; //response("rossz adatok megadva",403);}
//}}}}}
//if($request->has("id")){ $zene=zenemodel::find( $request->input('id'));}
//else if($request->has('zeneurl')){$zene=zenemodel::where('zeneurl',$request->input('zeneurl'));}
//else{ $zene=zenemodel::where('keresurl',$request->input('keresurl'));}
//if(empty($zene)){ return response('rossz adatok megadva',403); }
//$zene->delete();
try{
zenemodel::create($request->only(['zeneurl','eloado','cim','lejatszhatoe','hossz','tema','keresurl']));
return response('',204);}
catch(\Exception $e){return response("mar letezik ilyen ez a zene",403);}
}

public function idalapjankapottzeneadatai(Request $request,$id){
    try{

    $zenne=zenemodel::find($id);
    if(!$zenne){
        return response()->json("nincs ilyen id-ju zene",403,["Content-Type"=>"application/json"]);

    }
    return response()->json($zenne->toArray(),200,["Content-Type"=>"application/json"]);
    }
    catch (\Exception $e){return response()->json('hiba backenden',500,["Content-Type"=>'application/json']);}
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

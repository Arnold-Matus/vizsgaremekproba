<?php

namespace App\Http\Controllers;

use App\Models\keresmodel;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;
use PhpParser\Builder\Function_;

class kereskontroller extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function zenevalidacio(Request $request,$id){
         $token = $request->header( "token");
    if(empty($token)){ return response("nincs token megadva",404);}
     $felhasznalo= app(felhasznalokontroller::class)->tokenheztartozofelhasznalo($token);
if(!$felhasznalo){ return response("rossz token",404); }
if($felhasznalo->jog<4){ return response("nincs joga hozza",403); }

        $a =keresmodel::where("id",$id)->first();
        if(!$a) {
            return response()->json(["nincs ilyen keres",404,["Content-type"=>"application/json"]]);
        }
        $a->update(["validalte"=>true]);
        return response()->json("validalva",200,["Content-Type"=> "application/json"]);
    }
    pubLIC Function kerestorles(Request $request){
 $token = $request->header( "token");
    if(empty($token)){ return response("nincs token megadva",404);}
     $felhasznalo= app(felhasznalokontroller::class)->tokenheztartozofelhasznalo($token);
if(!$felhasznalo){ return response("rossz token",404); }
if($felhasznalo->jog<4){ return response("nincs joga hozza",403); }
//$validalt=$request->validate([]);
//$validalt = Validator::make($request->all(), [['keresurl'=>['required|regex:@(^(([http])|(https)){1}[:]{1}[//]{1}.+$)|(^$)@']],['zeneurl'=>['required|regex:@(^[C-Z]{1}:[\\]{1}.+[\\]{1}.*[\\]{1}.+[.]{1}mp3$)|^$@']],'cim'=>'sometimes|nullable','eloado'=>'sometimes|nullable','lejatszatoe'=>'sometimes|nullable|numeric|min:0|max:1','hossz'=>'sometimes|nullable|numeric|min:1','tema'=>'sometimes|nullable']);
$validalt = Validator::make($request->all(), ['id'=>'required_without_all:mikor|numeric|min:1',['mikor'=>['required_without_all:id|date']]]);
if($validalt->fails()){return response("rossz adatok megadva",403);}
if($request->has("id")){$keres=keresmodel::find($request->input('id'));}
else /*if ($request->has('mikor'))*/{$keres=keresmodel::where('mikor',$request->input('mikor'))->first();}
//esle {}
//$keres=keresmodel::where('keresurl');
$keres->delete();
return response('',204);

    }
    


    public function bekeres(Request $request){

    $url=$request->input('zeneurl');
    $token = $request->header( "token");
    if(empty($token)){ return response("nincs token megadva",404);}
     $felhasznalo= app(felhasznalokontroller::class)->tokenheztartozofelhasznalo($token);
if(!$felhasznalo){ return response("rossz token",404); }
if($felhasznalo->jog<2){ return response("nincs joga hozza",403); }
//$validalt=$request->validate([]);
//$validalt = Validator::make($request->all(), [['keresurl'=>['required|regex:@(^(([http])|(https)){1}[:]{1}[//]{1}.+$)|(^$)@']],['zeneurl'=>['required|regex:@(^[C-Z]{1}:[\\]{1}.+[\\]{1}.*[\\]{1}.+[.]{1}mp3$)|^$@']],'cim'=>'sometimes|nullable','eloado'=>'sometimes|nullable','lejatszatoe'=>'sometimes|nullable|numeric|min:0|max:1','hossz'=>'sometimes|nullable|numeric|min:1','tema'=>'sometimes|nullable']);
//$validalt = validator::make($request->all(),["zeneurl"=>]);
//if($validalt->fails()){return response("rossz adatok megadva",403);}
//if(!preg_match('@(^((http)|(https)){1}[:]{1}[\/]{2}.+[\/]{1}.+$)|(^$)|(^[C-Z]{1}:[\\]{1}.+[\\]{1}.*[\\]{1}.+[.]{1}mp3$)|(^zenek{1}[\\]{1}.+[.]{1}mp3$)@',$url)){
 //return response('rossz url megadva',400);}
 //if(!preg_match('@(^((http)|(https)){1}[:]{1}[\/]{2}.+[\/]{1}.+$)|(^[C-Z]{1}:[\\]{1}.+[\\]{1}.*[\\]{1}.+[.]{1}mp3$)|(^zenek{1}[\\]{1}.+[.]{1}mp3$)@',$url)){
  //  return response("rossz zeneurl",400);
 //}
keresmodel::create(['felhasznaloid'=>$felhasznalo->id,'zeneurl'=>$url,'validalte'=>0,'mikor'=>now()]);
return response('',204);   
}
public function kereseklistazasa(Request $request){
    $token = $request->header( "token");
    if(empty($token)){ return response("nincs token megadva",404);}
     $felhasznalo= app(felhasznalokontroller::class)->tokenheztartozofelhasznalo($token);
if(!$felhasznalo){ return response("rossz token",404); }
if($felhasznalo->jog<3){ return response("nincs joga hozza",403); }
return response()->json(keresmodel::all(),200,['Content-Type'=>'application/json']);
}
    public function index()
    {
        //
    }

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
    public function show(keresmodel $keresmodel)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, keresmodel $keresmodel)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(keresmodel $keresmodel)
    {
        //
    }
}

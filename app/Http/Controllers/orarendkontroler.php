<?php

namespace App\Http\Controllers;

use App\Models\orarendmodel;
use Carbon\Carbon;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;

class orarendkontroler extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function index()
    {
        //
    }
    public function jelenlegizene(Request $r){
    //nincs validacio, token publikus
    $talanmostani=orarendmodel::where("meddig",">=",Carbon::now())->orderBy("mikor")->limit(1)->first();

    if(empty($talanmostani)){return response("nincs",404);}
    if($talanmostani->mikor>=Carbon::now()){return response($talanmostani->zeneurl,"200");}
    }
    public function lejatszastorles(Request $request){

 $token = $request->header( "token");
    if(empty($token)){ return response("nincs token megadva",404);}
     $felhasznalo= app(felhasznalokontroller::class)->tokenheztartozofelhasznalo($token);
if(!$felhasznalo){ return response("rossz token",404); }
if($felhasznalo->jog<4){ return response("nincs joga hozza",403); }
//$validalt=$request->validate([]);
$validalt = Validator::make($request->all(), ['id'=>'required_without_all:mikortol|numeric|min:1',['mikortol'=>['required_without_all:id|date']]]);
if($validalt->fails()){return response("rossz adatok megadva",403);}
if($request->has("id")) $lejatszas=orarendmodel::find($request->input('id'));
else if($request->has('mikortol')) $lejatszas=orarendmodel::where('mikortol',$request->input('mikortol'));
if(!($lejatszas)){ return response('nincs ilyen rekord',404); }
$lejatszas->delete();
return response('',204);
    }
    public function mainapiorarend(Request $request){

//$token = $request->header( "token");
  //  if(empty($token)){ return response()->json("nincs token megadva",404);}
 //    $felhasznalo= app(felhasznalokontroller::class)->tokenheztartozofelhasznalo($token);
//if(!$felhasznalo){ return response()->json("rossz token",404); }
//if($felhasznalo->jog<4){ return response("nincs joga hozza",403); }
//$validalt=$request->validate([]);
//$validalt = Validator::make($request->all(), ['id'=>'required_without_all:mikortol|numeric|min:1',['mikortol'=>['required_without_all:id|date']]]);
//if($validalt->fails()){return response("rossz adatok megadva",403);}
$maiorarend= orarendmodel::whereBetween('mikortol',[today()->startOfDay(),today()->endOfDay()]);
return response()->json($maiorarend,200,['Content-Type'=>'application/json']);
    }
    public function teljesorarend(Request $request){

$token = $request->header( "token");
    if(empty($token)){ return response()->json("nincs token megadva",404);}
     $felhasznalo= app(felhasznalokontroller::class)->tokenheztartozofelhasznalo($token);
if(!$felhasznalo){ return response()->json("rossz token",404); }
if($felhasznalo->jog<2){ return response("nincs joga hozza",403); }
//$validalt=$request->validate([]);
//$validalt = Validator::make($request->all(), ['id'=>'required_without_all:mikortol|numeric|min:1',['mikortol'=>['required_without_all:id|date']]]);
//if($validalt->fails()){return response("rossz adatok megadva",403);}
$teljes= orarendmodel::all();
return response()->json($teljes,200,['Content-Type'=>'application/json']);
    }
    public function kovetkezolejatszas(Request $request){
 //    $token = $request->header( "token");
 //   if(empty($token)){ return response("nincs token megadva",404);}
 //    $felhasznalo= app(felhasznalokontroller::class)->tokenheztartozofelhasznalo($token);
//if(!$felhasznalo){ return response("rossz token",404); }
//if($felhasznalo->jog<4){ return response("nincs joga hozza",403); }
$kovetkezo=orarendmodel::where('mikortol','>',now())->orderBy('mikortol')->first();//->orderByDesc('mikortol')->first();
return response()->json($kovetkezo,200,['Content-Type'=> 'application/json']);
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
    public function show(orarendmodel $orarendmodel)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, orarendmodel $orarendmodel)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(orarendmodel $orarendmodel)
    {
        //
    }
}

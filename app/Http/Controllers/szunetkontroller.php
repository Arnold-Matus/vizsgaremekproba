<?php

namespace App\Http\Controllers;

use App\Models\keresmodel;
use App\Models\szunetekmodel;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;
//use function PHPUnit\Framework\isNumeric;

class szunetkontroller extends Controller
{
    /**
     * Display a listing of the resource.
     */
    public function szuneteklistaja(Request $request){
return response()->json(szunetekmodel::all(),200,['Content-Type'=>'application/json']);
    }
    public function szunettorles(Request $request,$hanyadik){

 $token = $request->header( "token");
    if(empty($token)){ return response("nincs token megadva",404);}
     $felhasznalo= app(felhasznalokontroller::class)->tokenheztartozofelhasznalo($token);
if(!$felhasznalo){ return response("rossz token",404); }
if($felhasznalo->jog<4){ return response("nincs joga hozza",403); }
//$validalt=$request->validate([]);
//$validalt = Validator::make($request->all(), ['id'=>'required_without_all:mikortol|numeric|min:1',['mikortol'=>['required_without_all:id|date']]]);
if(is_numeric($hanyadik)){return response("hanyadik szunet nincs megfeleloen megadva",403);}
 $szunett=szunetekmodel::where('hanyadik',$hanyadik);
//else if($request->has('mikortol')) $lejatszas=orarendmodel::where('mikortol',$request->input('mikortol'));
if(!($szunett)){ return response('nincs ilyen rekord',404); }
$szunett->delete();
return response('',204);
    }
    public function szunethozzaadas(Request $request){
     $token = $request->header( "token");
    if(empty($token)){ return response("nincs token megadva",404);}
     $felhasznalo= app(felhasznalokontroller::class)->tokenheztartozofelhasznalo($token);
if(!$felhasznalo){ return response("rossz token",404); }
if($felhasznalo->jog<4){ return response("nincs joga hozza",403); }
//$validalt=$request->validate([]);
$validalt = Validator::make($request->all(), ['hanyadik'=>'required|numeric|min:1',['kezdes'=>['required|date']],'vege'=>'required|date']);
//if(is_numeric($hanyadik)){return response("hanyadik szunet nincs megfeleloen megadva",403);}
if($validalt->fails()){return response('rossz adatok megadva',400);}
//else if($request->has('mikortol')) $lejatszas=orarendmodel::where('mikortol',$request->input('mikortol'));
//if(!($szunett)){ return response('nincs ilyen rekord',404); }
//$szunett->delete();
szunetekmodel::create($request->all());
return response('',204);
    }
    public function szunetmodositas(Request $request,$hanyadik){
         $token = $request->header( "token");
    if(empty($token)){ return response("nincs token megadva",404);}
     $felhasznalo= app(felhasznalokontroller::class)->tokenheztartozofelhasznalo($token);
if(!$felhasznalo){ return response("rossz token",404); }
if($felhasznalo->jog<4){ return response("nincs joga hozza",403); }
//$validalt=$request->validate([]);
$validalt = Validator::make($request->all(), [['kezdes'=>['sometimes|date']],'vege'=>'sometimes|date']);
if(is_numeric($hanyadik)){return response("hanyadik szunet nincs megfeleloen megadva",403);}
 $szunett=szunetekmodel::where('hanyadik',$hanyadik);
//else if($request->has('mikortol')) $lejatszas=orarendmodel::where('mikortol',$request->input('mikortol'));
if(!($szunett)){ return response('nincs ilyen rekord',404); }
$szunett->update($request->all());
return response('',204);
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

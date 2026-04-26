<?php

namespace App\Http\Controllers;

use App\Models\felhasznalomodel;
use DB;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Validator;

class felhasznalokontroller extends Controller
{
    /**
     * Display a listing of the resource.
     */
     /**$table->engine='InnoDB';
          //  $table->unsignedBigInteger('felhasznaloid')->primary()->autoIncrement();
          $table->id(); 
          $table->text('jelszoh')->comment('hashben');//->nullable();
            $table->text('nev')->unique();
            $table->text('email')->nullable();
            $table->boolean('letiltott')->default(false);
            $table->enum('jog',["demo","normalis","admin","tanar","asztali"]);//->nullable();
            //torolve bool oszlop
            $table->string("omazonosito",11)->nullable();
            $table->timestamps(6); */
    public function index()
    {
        //
    }
    public function tokenkeszites( felhasznalomodel $felhasznalomodel, $meddig){
    $tokenvege=$meddig==0?  now()->addHours(3): $meddig;
   // if($meddig == 0 ){$meddig = now()->addHours(3);}
        $felhasznalomodel->update(["token"=>uniqid(),"tokenvaliditasanakvege"=>$tokenvege]);
        
    return $felhasznalomodel;
        }
public function tokenheztartozofelhasznalo($token){
    return felhasznalomodel::where("token",$token)->first();

}
    public function bejelentkezes(Request $request){
   /*     $token= $request->headers("token");
        if($token== "" | $token == null){ return response("nincs token",403);}
       // else{ return response("",0); }
       $felhasznalo = felhasznalomodel::where("token",$token)->first();
       if()*/ //bejelnetkezesnel nincs meg token
       $email= base64_decode( $request->email,true);
       $jelszo=base64_decode($request->jelszo,true);
       if(!$request->has("email")|| ! $request->has("jelszo")){ return response("nincs minden adat megadva",401); }
       $felhasznalo = felhasznalomodel::where("email",$email)->first();
       if($felhasznalo==null){ return response("nincs ilyen felhasznalo",401); }
       if($felhasznalo->jelszoh==bcrypt(base64_decode(  $jelszo,true)) ){ return response("rossz jelszo",401); }
        $felhasznalo=$this->tokenkeszites($felhasznalo,0);
        return response($felhasznalo->token,200);
    }


    public function regisztracio(Request $request){

   /* function jogkezeles($jogg,Request $request){
    if($jogg=="admin" && $request->user()->jog=="admin"){

    }
    }
       // $_COOKIE[""] = $request->session()->get("");
   // $keresztnve=$request->keresztnev;
   // $vezeteknev=$request->vezeteknev;
    $validalas= $request->validate([]);
    if($validalas->fails()){
       // return redirect()->back()->withErrors($validalas->errors());
   return response("nem validalt",404);
       }
       else{
        felhasznalomodel::create(['jelszoh'=> md5($request->jelszo),'keresztnev'=>$request->keresztnev,'vezeteknev'=>$request->vezeteknev,'email'=>$request->email,'jog'=>$request->jog,'omazonosito'=>$request->omazonosito,'created_at'=>now()]);//\Auth::user()->id,''=>$validalas->id]);
       }*/
// $jelszregex="@(^[-A-Za-z0-9+/=]|=[^=]|={3,}$)@";
    //  $validalt=$request->validate(['email'=>'required|unique:felhasznalo|email','jelszo'=>'required|regex:(^[-A-Za-z0-9+/=]|=[^=]|={3,}$)|min:6','keresztnev'=>'required|min:2|regex:(^[\p{L}+]$)','vezeteknev'=>'required|min:3|regex:(^[\p{L}+]$)','omazonosito'=>'sometimes|min:11|max:11|regex:(^[\d]{11}$)']); //https://stackoverflow.com/questions/475074/regex-to-parse-or-validate-base64-data
      $validalt=Validator::make( $request->all(),['email'=>'required|unique:felhasznalo|email',['jelszo'=>['required|min:6|regex:@(^[-A-Za-z0-9+/=]|=[^=]|={3,}$)@']],'keresztnev'=>'required|min:2|regex:/(^[\p{L}]+$)/','vezeteknev'=>'required|min:3|regex:/(^[\p{L}]+$)/','omazonosito'=>'sometimes|min:11|max:11|regex:/(^[\d]{11}$)/']); //https://stackoverflow.com/questions/475074/regex-to-parse-or-validate-base64-data
      //if($validalt->fails())
      if($validalt->fails()){
       // { return response()->json($validalt->errors(),403,["Content-Type"=>"applocation/json"]); }
        return response()->json($validalt->errors(),403,["Content-Type"=>"applocation/json"]); }
        try{
          // return response($validalt->getData()['jelszo'],301);
         //   felhasznalomodel::create(["email"=>$validalt->safe(["email"]),'keresztnev'=>$validalt['keresztnev'],'vezeteknev'=>$validalt['vezeteknev'],'jelszoh'=>bcrypt(base64_decode( $validalt['jelszoh'],true) ),'jog'=>2,'omazonosito'=>$validalt['omazonosito']]);
          felhasznalomodel::create(["email"=>$validalt->getData()["email"],'keresztnev'=>$validalt->getData()['keresztnev'],'vezeteknev'=>$validalt->getData()['vezeteknev'],'jelszoh'=>bcrypt(base64_decode( $validalt->getData()['jelszo'],true) ),'jog'=>2,'omazonosito'=>$validalt->getData()['omazonosito']]);
       return response('regisztralva',201);
            }
    catch (\Exception $e) { return response($e->getMessage(),500); }
      }
      public function regisztraciobarmilyenjogut(Request $request){

      $token=$request->header('token');
      if(!$token) {return response("nincs token megadva",404);}
      $felhasznalo= $this->tokenheztartozofelhasznalo( $token );
      if(!$felhasznalo){return response("rossz token",404); }

      
   /* function jogkezeles($jogg,Request $request){
    if($jogg=="admin" && $request->user()->jog=="admin"){

    }
    }
       // $_COOKIE[""] = $request->session()->get("");
   // $keresztnve=$request->keresztnev;
   // $vezeteknev=$request->vezeteknev;
    $validalas= $request->validate([]);
    if($validalas->fails()){
       // return redirect()->back()->withErrors($validalas->errors());
   return response("nem validalt",404);
       }
       else{
        felhasznalomodel::create(['jelszoh'=> md5($request->jelszo),'keresztnev'=>$request->keresztnev,'vezeteknev'=>$request->vezeteknev,'email'=>$request->email,'jog'=>$request->jog,'omazonosito'=>$request->omazonosito,'created_at'=>now()]);//\Auth::user()->id,''=>$validalas->id]);
       }*/
// $jelszregex="@(^[-A-Za-z0-9+/=]|=[^=]|={3,}$)@";
    //  $validalt=$request->validate(['email'=>'required|unique:felhasznalo|email','jelszo'=>'required|regex:(^[-A-Za-z0-9+/=]|=[^=]|={3,}$)|min:6','keresztnev'=>'required|min:2|regex:(^[\p{L}+]$)','vezeteknev'=>'required|min:3|regex:(^[\p{L}+]$)','omazonosito'=>'sometimes|min:11|max:11|regex:(^[\d]{11}$)']); //https://stackoverflow.com/questions/475074/regex-to-parse-or-validate-base64-data
      $validalt=Validator::make( $request->all(),['email'=>'required|unique:felhasznalo|email',['jelszo'=>['required|min:6|regex:@(^[-A-Za-z0-9+/=]|=[^=]|={3,}$)@']],'keresztnev'=>'required|min:2|regex:/(^[\p{L}]+$)/','vezeteknev'=>'required|min:3|regex:/(^[\p{L}]+$)/','omazonosito'=>'sometimes|min:11|max:11|regex:/(^[\d]{11}$)/','jog'=>'required|regex:/(^[\d]{1}$)/']); //https://stackoverflow.com/questions/475074/regex-to-parse-or-validate-base64-data
      //if($validalt->fails())
      if($validalt->fails()){
       // { return response()->json($validalt->errors(),403,["Content-Type"=>"applocation/json"]); }
        return response()->json($validalt->errors(),403,["Content-Type"=>"applocation/json"]); }
        try{
          // return response($validalt->getData()['jelszo'],301);
         //   felhasznalomodel::create(["email"=>$validalt->safe(["email"]),'keresztnev'=>$validalt['keresztnev'],'vezeteknev'=>$validalt['vezeteknev'],'jelszoh'=>bcrypt(base64_decode( $validalt['jelszoh'],true) ),'jog'=>2,'omazonosito'=>$validalt['omazonosito']]);
         if($felhasznalo->jog<$validalt->getData()["jog"]){return response("nincs ehhez joga",403);}
         felhasznalomodel::create(["email"=>$validalt->getData()["email"],'keresztnev'=>$validalt->getData()['keresztnev'],'vezeteknev'=>$validalt->getData()['vezeteknev'],'jelszoh'=>bcrypt(base64_decode( $validalt->getData()['jelszo'],true) ),'jog'=>$validalt->getData()['jog'],'omazonosito'=>$validalt->getData()['omazonosito']]);
       return response('regisztralva',201);
            }
    catch (\Exception $e) { return response($e->getMessage(),500); }
      }
public function felhasznalotorlesemailalapjan(Request $request,$email){
$token=$request->header('token');
if(!$token){ return response('nincs token megadva',404); }
$felhasznalo = $this->tokenheztartozofelhasznalo( $token ); 
if(!$felhasznalo){ return response('rossz token',404); }
if($felhasznalo->jog<4){ return response('nincs ehez joga',403); }
$torlendofelhasznalo= felhasznalomodel::where('email',$email)->first();
if(!$torlendofelhasznalo){return response('nincs ilyen felhasznalo',404);}
$torlendofelhasznalo->delete();
return response($email.' sikeresen torolve',200);
}
public function kilepes(Request $request)//logout/tokeneltuntetes
{
$token=$request->header('token');
if(!$token){ return response('nincs token megadva',404); }
$felhasznalo = $this->tokenheztartozofelhasznalo( $token ); 
if(!$felhasznalo){ return response('rossz token',404); }
//if($felhasznalo->jog<2){ return response('nincs ehez joga',403); }
//$torlendofelhasznalo= felhasznalomodel::where('email',$email)->first();
if(!$felhasznalo){return response('nincs ilyen felhasznalo',404);}
$felhasznalo->update(['token'=>null,'tokenvaliditasanakvege'=>now()]);
return response(' sikeresen kijelentkezve',200);

}
public function kileptetesemailalapjan(Request $request,$email){
    $token=$request->header('token');
if(!$token){ return response('nincs token megadva',404); }
$felhasznalo = $this->tokenheztartozofelhasznalo( $token ); 
if(!$felhasznalo){ return response('rossz token',404); }
if($felhasznalo->jog<3){ return response('nincs ehez joga',403); }//tanarok is tudjanak kileptetni embereket
$kileptendofelhasznalo= felhasznalomodel::where('email',$email)->first();
if(!$kileptendofelhasznalo){return response('nincs ilyen felhasznalo',404);}
$kileptendofelhasznalo->update(['token'=>null,'tokenvaliditasanakvege'=>now()]);;
return response($email.' sikeresen kileptetve',200);
}
public function jelenlegifelhasznalotorlese(Request $request){
$token=$request->header('token');
if(!$token){ return response('nincs token megadva',404); }
$felhasznalo = $this->tokenheztartozofelhasznalo( $token ); 
if(!$felhasznalo){ return response('rossz token',404); }
if($felhasznalo->jog<4){ return response('nincs ehez joga',403); }
//$torlendofelhasznalo= felhasznalomodel::where('email',$email)->first();
if(!$felhasznalo){return response('nincs ilyen felhasznalo',404);}
$felhasznalo->delete();
return response('ont sikeresen toroluk',200);
}
public function aktivfelhasznaloszam(Request $request){
   //return $this->jadwal($request);
   return  response(  DB::select('SELECT * from aktivfelhasznalok'),200);

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
    public function show(felhasznalomodel $felhasznalomodel)
    {
        //
    }

    /**
     * Update the specified resource in storage.
     */
    public function update(Request $request, felhasznalomodel $felhasznalomodel)
    {
        //
    }

    /**
     * Remove the specified resource from storage.
     */
    public function destroy(felhasznalomodel $felhasznalomodel)
    {
        //
    }
}

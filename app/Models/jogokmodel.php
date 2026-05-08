<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class jogokmodel extends Model
{
    //
      public $table = "jogok";
    public $fillable = ["szint","jog"];
    public $timestamps=false;
    public function felhasznalo(){
    //return $this->belongsTo(felhasznalomodel::class,"felhasznaloid","id");
   return $this->hasMany(felhasznalomodel::class,"jog","szint");
    }
}

<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class szunetmodel extends Model
{
    //
     public $table = "szunetek";
    public $fillable = ["hanyadik","kezdes","vege"];
    public $timestamps=false;

}

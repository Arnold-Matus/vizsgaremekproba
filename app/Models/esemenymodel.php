<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class esemenymodel extends Model
{
    //
    public $table = "esemeny";
    protected $fillable = ["sqlesemeny","hol","regisor","ujsor","CREATED_AT ","UPDATED_AT","ki"];
    protected $timestamps=true;
}

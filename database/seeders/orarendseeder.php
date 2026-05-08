<?php

namespace Database\Seeders;

use DB;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class orarendseeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //
        DB::table("orarend")->insert(["zeneid"=>1,"mikortol"=>'2000-01-01 01:01:01',"meddig"=>"2026-05-22 08:00:00"]);
    }
}

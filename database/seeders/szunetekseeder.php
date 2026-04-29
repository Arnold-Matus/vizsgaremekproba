<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use DB;
class szunetekseeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //
        DB::unprepared('DELETE from szunetek;

set @ma= timestamp(CURRENT_DATE);

INSERT INTO szunetek

VALUES


(1,addtime( @ma,"8:30:00"),ADDTIME(@ma,"8:40:00")),
  (2,ADDTIME( @ma,"9:25:00"),ADDTIME(@ma,"9:35:00")),
(3,ADDTIME( @ma,"10:20:00"),ADDTIME(@ma,"10:35:00")),
(4,ADDTIME( @ma,"11:20:00"),ADDTIME(@ma,"11:30:00")),
 (5,ADDTIME( @ma,"12:15:00"),ADDTIME(@ma,"12:25:00")),
(6,ADDTIME( @ma,"13:10:00"),ADDTIME(@ma,"13:35:00")),
(7,ADDTIME( @ma,"14:20:00"),ADDTIME(@ma,"14:25:00"));
');
    }
}

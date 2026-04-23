<?php

namespace Database\Seeders;

use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;
use DB;
class seeder1 extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        //
      //  DB::table("jogok")->insert([["szint"=>1,"jog"=>"demo"],["szint"=>2,"jog"=>"normal"],["szint"=>3,"jog"=>"tanar"],["szint"=>4,"jog"=>"admin"]]);
        DB::table("felhasznalo")->insert([["email"=>"suliradio004@gmail.com","vezeteknev"=>"suli","keresztnev"=>"radio","omazonosito"=>10111111111,"jelszoh"=>bcrypt("suliradio"),"token"=>null,"tokenvaliditasanakvege"=>null,"letiltott"=>false,"emailverifikalva"=>true,"jog"=>4],["email"=>"r.botond.ujk@gmail.com","vezeteknev"=>"Rákóczi","keresztnev"=>"Botond","omazonosito"=>8266111116,"jelszoh"=>bcrypt("VSszMDFD"),"token"=>null,"tokenvaliditasanakvege"=>null,"letiltott"=>false,"emailverifikalva"=>true,"jog"=>4],["email"=>"assassinzsolti06@gmail.com","vezeteknev"=>"Sütő","keresztnev"=>"Zsolt Márk","omazonosito"=>11583122109,"jelszoh"=>bcrypt("zsoltijelszava"),"token"=>null,"tokenvaliditasanakvege"=>null,"letiltott"=>false,"emailverifikalva"=>true,"jog"=>4],["email"=>"arnoldmatus300@gmail.com","vezeteknev"=>"Matus","keresztnev"=>"Arnold","omazonosito"=>7254305155,"jelszoh"=>bcrypt("39c99b36d4fac56afe17d5ff28eaf987652408a338fd2adfb485a3c1b93649caf2f6720d2d632337f34152eeca44a36196ad8e2c280f35efd60e40e5ad40a88e"),"token"=>null,"tokenvaliditasanakvege"=>null,"letiltott"=>false,"emailverifikalva"=>true,"jog"=>4]]);
    }
}

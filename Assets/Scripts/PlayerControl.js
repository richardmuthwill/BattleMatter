#pragma strict

/*
  Animation Name Help - Examples

  Shoot: 
  Draw: Draw_[stance]_To_[draw]_[weapon]
*/

var stance 		: String =	"Stand"; // "Standing", "Crouch", "Prone", "Climb"
var action 		: String =	"Idle"; // "Idle", "Shoot", "Walk", "Death"
var direction 	: String =	"Forward"; // "Forward", "Back", "Left", "Right", "Forward_Right", "Forward_Left"
var weapon 		: String =	"Aimed_SH"; // "Aimed_SH": Single Handed, "Aimed_DH": Double Handed, "Aimed_ShotgunSniper_DH": Alternate Double Handed
var draw		: String =	"Pistol"; // "Pistol", "Auto", "ShotgunSniper"
var ladder 		: String =	"Ladder_Down"; // "Ladder_Down", "Ladder_Up";

var stanceani	: boolean =	false;

function Start () {

}

function Update () {

}
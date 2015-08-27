<?php
	require_once("config.php");

	$auth_host = $GLOBALS['auth_host'];
	$auth_user = $GLOBALS['auth_user'];
	$auth_pass = $GLOBALS['auth_pass'];
	$auth_dbase = $GLOBALS['auth_dbase'];

	$db = mysql_connect($auth_host, $auth_user, $auth_pass) or die (mysql_error());
	mysql_select_db($auth_dbase,$db);
	
	$player = mysql_real_escape_string($_POST['name']);
	$itemData = mysql_real_escape_string($_POST['itemData']);

	mysql_query("DELETE FROM attributes WHERE player= '$player'");
	mysql_query("INSERT INTO attributes(player,item) VALUES ('$player','$itemData')");

	mysql_close($db);
?> 
 
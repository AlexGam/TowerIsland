<?php
	require_once("config.php");

	$auth_host = $GLOBALS['auth_host'];
	$auth_user = $GLOBALS['auth_user'];
	$auth_pass = $GLOBALS['auth_pass'];
	$auth_dbase = $GLOBALS['auth_dbase'];

	$db = mysql_connect($auth_host, $auth_user, $auth_pass) or die (mysql_error());
	mysql_select_db($auth_dbase,$db);
	
	$player = mysql_real_escape_string($_POST['name']);
	$containerId= mysql_real_escape_string($_POST['id']);
	$itemData = mysql_real_escape_string($_POST['itemData']);

	mysql_query("DELETE FROM container WHERE player= '$player' AND containerid = '$containerId'");
	mysql_query("INSERT INTO container(player,item,containerid) VALUES ('$player','$itemData','$containerId')");

	mysql_close($db);
?> 
 
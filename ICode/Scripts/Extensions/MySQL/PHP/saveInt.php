<?php
	require_once("config.php");

	$auth_host = $GLOBALS['auth_host'];
	$auth_user = $GLOBALS['auth_user'];
	$auth_pass = $GLOBALS['auth_pass'];
	$auth_dbase = $GLOBALS['auth_dbase'];

	$db = mysql_connect($auth_host, $auth_user, $auth_pass) or die (mysql_error());
	mysql_select_db($auth_dbase,$db);
	
	$key = mysql_real_escape_string($_POST['key']);
	$value = mysql_real_escape_string($_POST['value']);
	mysql_query("DELETE FROM ints WHERE name = '$key'");
	mysql_query("INSERT INTO ints(name,value) VALUES ('$key','$value')");
	mysql_close($db);
?> 
 
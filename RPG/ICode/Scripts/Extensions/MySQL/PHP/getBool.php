<?php
	require_once("config.php");

	$auth_host = $GLOBALS['auth_host'];
	$auth_user = $GLOBALS['auth_user'];
	$auth_pass = $GLOBALS['auth_pass'];
	$auth_dbase = $GLOBALS['auth_dbase'];

	$db = mysql_connect($auth_host, $auth_user, $auth_pass) or die (mysql_error());
	mysql_select_db($auth_dbase,$db);
	
	$key = mysql_real_escape_string($_POST['key']);
	$sql = mysql_query("SELECT value FROM bools WHERE name = '$key'");
	$row = mysql_fetch_row($sql);

 	echo $row[0];
	mysql_close($db);
?> 
 
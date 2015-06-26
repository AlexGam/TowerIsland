<?php
	require_once("config.php");

	$auth_host = $GLOBALS['auth_host'];
	$auth_user = $GLOBALS['auth_user'];
	$auth_pass = $GLOBALS['auth_pass'];
	$auth_dbase = $GLOBALS['auth_dbase'];
	
	$db = pg_connect($auth_host, $auth_user, $auth_pass) or die (pg_error());
	pg_select_db($auth_dbase,$db);
	$user_name=pg_real_escape_string($_POST['name']);
	$user_password=pg_real_escape_string($_POST['password']);

	$sql = pg_query("SELECT * FROM account WHERE (user = '$user_name' AND password = MD5('".$user_password."')) ") or die(pg_error());
	$rows= pg_num_rows($sql);
	if($rows > 0){
		echo "true";
	}else{
		echo "false";
	}
	pg_close($db);
	
?> 
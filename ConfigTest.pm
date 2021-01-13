# --
# Copyright (C) 2001-2019 OTRS AG, https://otrs.com/
# --
# This software comes with ABSOLUTELY NO WARRANTY. For details, see
# the enclosed file COPYING for license information (GPL). If you
# did not receive this file, see https://www.gnu.org/licenses/gpl-3.0.txt.
# --
#  Note:
#
#  -->> Most OTRS configuration should be done via the OTRS web interface
#       and the SysConfig. Only for some configuration, such as database
#       credentials and customer data source changes, you should edit this
#       file. For changes do customer data sources you can copy the definitions
#       from Kernel/Config/Defaults.pm and paste them in this file.
#       Config.pm will not be overwritten when updating OTRS.
# --

package Kernel::Config;

use strict;
use warnings;
use utf8;

sub Load {
    my $Self = shift;

    # ---------------------------------------------------- #
    # database settings                                    #
    # ---------------------------------------------------- #

    # The database host
    $Self->{'DatabaseHost'} = '127.0.0.1';

    # The database name
    $Self->{'Database'} = "otrs";

    # The database user
    $Self->{'DatabaseUser'} = "otrs";

    # The password of database user. You also can use bin/otrs.Console.pl Maint::Database::PasswordCrypt
    # for crypted passwords
    $Self->{'DatabasePw'} = 'password';

    # The database DSN for MySQL ==> more: "perldoc DBD::mysql"
    $Self->{'DatabaseDSN'} = "DBI:mysql:database=$Self->{Database};host=$Self->{DatabaseHost}";

    # The database DSN for PostgreSQL ==> more: "perldoc DBD::Pg"
    # if you want to use a local socket connection
#    $Self->{DatabaseDSN} = "DBI:Pg:dbname=$Self->{Database};";
    # if you want to use a TCP/IP connection
#    $Self->{DatabaseDSN} = "DBI:Pg:dbname=$Self->{Database};host=$Self->{DatabaseHost};";

    # The database DSN for Microsoft SQL Server - only supported if OTRS is
    # installed on Windows as well
#    $Self->{DatabaseDSN} = "DBI:ODBC:driver={SQL Server};Database=$Self->{Database};Server=$Self->{DatabaseHost},1433";

    # The database DSN for Oracle ==> more: "perldoc DBD::oracle"
#    $Self->{DatabaseDSN} = "DBI:Oracle://$Self->{DatabaseHost}:1521/$Self->{Database}";
#
#    $ENV{ORACLE_HOME}     = '/path/to/your/oracle';
#    $ENV{NLS_DATE_FORMAT} = 'YYYY-MM-DD HH24:MI:SS';
#    $ENV{NLS_LANG}        = 'AMERICAN_AMERICA.AL32UTF8';

    # ---------------------------------------------------- #
    # fs root directory
    # ---------------------------------------------------- #
    $Self->{Home} = '/opt/otrs';

    # ---------------------------------------------------- #
    # insert your own config settings "here"               #
    # config settings taken from Kernel/Config/Defaults.pm #
    # ---------------------------------------------------- #


    # Setup Logs ------------------------------------------#

$Self->{'LogModule'} = 'Kernel::System::Log::File'; 
$Self->{'LogModule::LogFile'} = '/opt/otrs/var/log/otrs.log';

    # ---------------------------------------------------- #
    # ---------------------------------------------------- #

    # Setup LDAP for agents

$Self->{'AuthModule'} = 'Kernel::System::Auth::LDAP';
$Self->{'AuthModule::LDAP::Host'} = 'DC-1';
$Self->{'AuthModule::LDAP::BaseDN'} = 'dc=DOMEN_NAME,dc=local';
$Self->{'AuthModule::LDAP::UID'} = 'sAMAccountName';
$Self->{'AuthModule::LDAP::GroupDN'} = 'cn=Acs-OTRSagents,OU=Access groups,OU=Main.Office,dc=DOMEN_NAME,dc=local';
$Self->{'AuthModule::LDAP::AccessAttr'} = 'member';
$Self->{'AuthModule::LDAP::UserAttr'} = 'DN';
$Self->{'AuthModule::LDAP::SearchUserDN'} = 'admin.otrs';
$Self->{'AuthModule::LDAP::SearchUserPw'} = 'password';
$Self->{'AuthModule::LDAP::AlwaysFilter'} = '';
$Self->{'AuthModule::LDAP::Params'} = {
port => 389,
timeout => 120,
async => 0,
version => 3,
sscope => 'sub'
};

$Self->{'AuthSyncModule'} = 'Kernel::System::Auth::Sync::LDAP';
$Self->{'AuthSyncModule::LDAP::Host'} = 'DC-1';
$Self->{'AuthSyncModule::LDAP::BaseDN'} = 'dc=DOMEN_NAME, dc=local';
$Self->{'AuthSyncModule::LDAP::UID'} = 'sAMAccountName';
$Self->{'AuthSyncModule::LDAP::SearchUserDN'} = 'admin.otrs';
$Self->{'AuthSyncModule::LDAP::SearchUserPw'} = 'password';
$Self->{'AuthSyncModule::LDAP::UserSyncMap'} = {
UserFirstname => 'givenName',
UserLastname => 'sn',
UserEmail => 'mail',
};


    # Setup LDAP for users


$Self->{'Customer::AuthModule'} = 'Kernel::System::CustomerAuth::LDAP';
$Self->{'Customer::AuthModule::LDAP::Host'} = 'DC-1';
$Self->{'Customer::AuthModule::LDAP::BaseDN'} = 'dc=DOMEN_NAME,dc=local';
$Self->{'Customer::AuthModule::LDAP::UID'} = 'sAMAccountName';
$Self->{'Customer::AuthModule::LDAP::SearchUserDN'} = 'admin.otrs';
$Self->{'Customer::AuthModule::LDAP::SearchUserPw'} = 'password';
$Self->{CustomerUser} = {
Module => 'Kernel::System::CustomerUser::LDAP',
Params => {
Host => 'DC-1',
BaseDN => 'DC=DOMEN_NAME,DC=local',
SSCOPE => 'sub',
UserDN =>'admin.otrs',
UserPw => 'password',
AlwaysFilter => '(&(samAccountType=805306368)(!(userAccountControl:1.2.840.113556.1.4.803:=2)))',
SourceCharset => 'utf-8',
DestCharset => 'utf-8',
},

CustomerKey => 'sAMAccountName',
CustomerID => 'mail',
CustomerUserListFields => ['sAMAccountName', 'cn', 'mail'],
CustomerUserSearchFields => ['sAMAccountName', 'cn', 'mail'],
CustomerUserSearchPrefix => '',
CustomerUserSearchSuffix => '*',
CustomerUserSearchListLimit => 10000,
CustomerUserPostMasterSearchFields => ['mail'],
CustomerUserNameFields => ['givenname', 'sn'],
Map => [
[ 'UserSalutation', 'Title', 'title', 1, 0, 'var' ],
[ 'UserFirstname', 'Firstname', 'givenname', 1, 1, 'var' ],
[ 'UserLastname', 'Lastname', 'sn', 1, 1, 'var' ],
[ 'UserLogin', 'Login', 'sAMAccountName', 1, 1, 'var' ],
[ 'UserEmail', 'Email', 'mail', 1, 1, 'var' ],
[ 'UserCustomerID', 'CustomerID', 'mail', 0, 1, 'var' ],
[ 'UserPhone', 'Phone', 'telephonenumber', 1, 0, 'var' ],
[ 'UserAddress', 'Address', 'postaladdress', 1, 0, 'var' ],
[ 'UserComment', 'Comment', 'description', 1, 0, 'var' ],
],
};

    # ---------------------------------------------------- #
    # ---------------------------------------------------- #
    #                                                      #
    # end of your own config options!!!                    #
    #                                                      #
    # ---------------------------------------------------- #
    # ---------------------------------------------------- #

    return 1;
}

# ---------------------------------------------------- #
# needed system stuff (don't edit this)                #
# ---------------------------------------------------- #

use Kernel::Config::Defaults; # import Translatable()
use parent qw(Kernel::Config::Defaults);

# -----------------------------------------------------#

1;

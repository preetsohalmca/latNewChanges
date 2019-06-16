@rem ---------------------------------------------------------------------------------
@rem Config transformations cmd 
@rem Created 11/07/2012 by Fabio Stawinski
@rem Volvo IT - ADT.NET
@rem 
@rem https://teamplace.volvo.com/sites/volvoit-dotNET/default.aspx
@rem ---------------------------------------------------------------------------------

@rem ---------------------------------------------------------------------------------
@rem Execute the transformations for each file needed
@rem Parameters: 
@rem      %1 - Path to ctt.exe tool 
@rem      %2 - Install path
@rem      %3 - Transformation to be applied (DEV, ST, SIT, QA or Prod)
@rem ---------------------------------------------------------------------------------

@rem ---------------------------------------------------------------------------------
@rem NHibernate.config
@rem ---------------------------------------------------------------------------------
%1 s:%2\NHibernate.config t:%2\Transformations\NHibernate.%3.Config d:%2\NHibernate.Transformed.Config"
attrib %2\NHibernate.config -r
copy /y %2\NHibernate.Transformed.Config %2\NHibernate.config 

@rem ---------------------------------------------------------------------------------
@rem POSProxy.WindowsService.exe.config
@rem ---------------------------------------------------------------------------------
%1 s:%2\POSProxy.WindowsService.exe.config t:%2\Transformations\App.%3.Config d:%2\App.Transformed.Config"
attrib %2\POSProxy.WindowsService.exe.config -r
copy /y %2\App.Transformed.Config %2\POSProxy.WindowsService.exe.config  

@rem ---------------------------------------------------------------------------------
@rem Deleting used files
@rem ---------------------------------------------------------------------------------

del %2\NHibernate.Transformed.Config
del %2\App.Transformed.Config
del %1
del %1.config 
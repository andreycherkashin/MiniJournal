Push-Location $PSScriptRoot

$Autofac = [reflection.assembly]::LoadFrom($PSScriptRoot + "\Autofac.dll") 

$OnAssemblyResolve = [System.ResolveEventHandler] {
  param($sender, $e)

  # from:Autofac, Version=4.1.0.0, Culture=neutral, PublicKeyToken=17863AF14B0044DA
  # to:  Autofac, Version=4.8.1.0, Culture=neutral, PublicKeyToken=17863AF14B0044DA
  if ($e.Name -eq "Autofac, Version=4.1.0.0, Culture=neutral, PublicKeyToken=17863AF14B0044DA") { return $Autofac }

  foreach($a in [System.AppDomain]::CurrentDomain.GetAssemblies())
  {
    if ($a.FullName -eq $e.Name)
    {
      return $a
    }
  }

  return $null
}

[System.AppDomain]::CurrentDomain.add_AssemblyResolve($OnAssemblyResolve)

$PackageRoot = $PSScriptRoot

$LoadingModule = $true

dir *.ps1 | % Name | Resolve-Path | Import-Module 

$LoadingModule = $false

Pop-Location

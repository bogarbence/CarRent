   M S B A M L   `   `   `  ����=  9VU, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:VU.ViewsVU.Views  5          VU.Views.CarStatusView   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      (localclr-namespace:VU.Views     5      ^ ZCaliburn.Micro.Platform, Version=3.2.0.0, Culture=neutral, PublicKeyToken=8e5891231f2ed21f(calhttp://www.caliburnproject.org  5      .��5
      � 
�������$	��450��6   $	��800��6+   .+�5   
   �� $��	20,10,0,0q�6   $��Modell:��6#   �� 5   
   -   5        $��20��5      $	��150��6    $
��Left=�6,   $	��Top=�6G   $��
100,13,0,0q�6_     �� DisplayMemberPath$  modell��6�     Caliburn.Micro.Message   Attach$> 8[Event SelectionChanged] = [Action Selected($eventArgs)]��6�   ��6s   �� *	CarList+6+  �� 5   
   $��	20,40,0,0q�6   $��Status:��6#   �� 5   
   -   5        $��20��5      $	��150��6    $
��Left=�6,   $	��Top=�6G   $��
100,43,0,0q�6_   $D >[Event SelectionChanged] = [Action SelectedStatus($eventArgs)]��6s   .@�5      �� $��Broken��6   �� 5      $��Intact��6   5      �� 5   
   -   5         ��Name$ ChangeStatusBtn��5      $��20��6*   $	��100��66   $
��Left=�6B   $��	20,70,0,0q�6]   $	��Top=�6p   .��6�   Change Status6�   5      5      
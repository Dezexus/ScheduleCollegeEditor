   M S B A M L   `   `   `  ����P  LScheduleCollegeEditor, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ScheduleCollegeEditor.ViewScheduleCollegeEditor.View  5   	   9    3ScheduleCollegeEditor.View.TrashDeletedLessonWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:ScheduleCollegeEditor.View     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   b ^GalaSoft.MvvmLight.Platform, Version=5.4.1.0, Culture=neutral, PublicKeyToken=5f873c45e98af8a1.command http://www.galasoft.ch/mvvmlight  5   	   -   5          �Name$  Window��5	   	      Window.Static.Background#���   5
   	    � Title$' !История изменений��5   	    � WindowStyle$ SingleBorderWindow=�5   	    � 
ResizeMode$ CanMinimize=�5   	    � WindowStartupLocation$ CenterOwner=�5   	   $	��450��5   	   $	��400��5   	   $	��350��5   	   $	��370��5   	   .��5      �  � RowDefinitions 5   
   �� 5      �� 5      $��30 �6   �� 5      $��60 �6   .+�5   
   �� $��Center=�6     ListBoxBase#���  6]     
ItemsStyle#���  6�   $��10q�6�   ��66   �� *LessonCardList+ �� SelectedIndex 5      �� *SelectedIndex+ �� SelectedValue 6:   �� *SelectedLessonCard+.  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5      	   	EventName$	 MouseDoubleClick��6!   .  (System.Windows.Interactivity.TriggerBase
   Actions.
 5       /  )GalaSoft.MvvmLight.Command.EventToCommand     Command 6-   �� *RestoreLessonCommand+   CommandParameter 6W   ��  �� ElementName$ Window��5!      5#      �� 5%   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $
��10,0q�6;   $��Center=�6I   .+�5&      �� $ 
Horizontal=�6   $	��5,0q�62   .+�5'      A� ���#c96363$��15��6)   $��15��64   �� 5(      $!��Удалённые пары��6   $
��10,0q�62     TextBlock.Static.Foreground#���  6@   5)      �� 5*      $ 
Horizontal=�6   $	��5,0q�62   .+�5+      A� ���#ffda91$��15��6)   $��15��64   �� 5,      $#��Изменённые пары��6   $
��10,0q�63   #���  6A   5-      5.      �� 51   
   $��Восстановить��6   $
��10,0q�6(   $��2��66   $��35��6C     
ButtonBase#���  6O   ��52      �� *RestoreLessonCommand+��6:   �� $ Window��54      55      
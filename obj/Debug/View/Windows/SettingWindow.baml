   M S B A M L   `   `   `  ����P  LScheduleCollegeEditor, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ScheduleCollegeEditor.ViewScheduleCollegeEditor.View  5   	   Q-clr-namespace:ScheduleCollegeEditor.ViewModelScheduleCollegeEditor.ViewModel  5   	   .    (ScheduleCollegeEditor.View.SettingWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:ScheduleCollegeEditor.View     5   	   C	viewmodel-clr-namespace:ScheduleCollegeEditor.ViewModel     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5   	   b ^GalaSoft.MvvmLight.Platform, Version=5.4.1.0, Culture=neutral, PublicKeyToken=5f873c45e98af8a1.command http://www.galasoft.ch/mvvmlight  5	   	     � Title$  Настройки��5   	   $	��450��5   	   $	��580��5   	   $	��450��5   	   $	��580��5   	    � 
ResizeMode$ CanMinimize=�5   	      Window.Static.Background#���   5   	    � WindowStyle$ SingleBorderWindow=�5   	    � WindowStartupLocation$ CenterOwner=�5   	   .  (System.Windows.Interactivity.Interaction   Triggers 5      /  )System.Windows.Interactivity.EventTrigger  5   
      	EventName$ Loaded��6   .  (System.Windows.Interactivity.TriggerBase   Actions. 5      /  )GalaSoft.MvvmLight.Command.EventToCommand     Command 6%   �� *LoadedAppCommand+5       � DataContext 5      <  6ScheduleCollegeEditor.ViewModel.SettingWindowViewModel  5   
   .��5      � 	 � RowDefinitions	 5    
   �� 5!      $
��auto �6   �� 5"      $
��auto �6   �� 5#      $
��auto �6   �� 5$      $
��auto �6   .+�5'   
   � $��0��6   $��10,20,10,10q�6   .+�5(      ��   
BorderBase#���  6   
 J� Effect
 5)      3  -System.Windows.Media.Effects.DropShadowEffect  5*         Opacity$	 0.1Q�6'      ShadowDepth$ 3Q�65      
BlurRadius$ 2Q�6E   5,      �� 5-      $��10q�6   .+�5.      �� $��Внешний вид��6   $��14(�6/   $��Center=�6=   $
��10,5q�6Z     TextBlock.Static.Foreground#���  6h   �� 5/      $��1��6   
�������$��5,0,5,10q�66   �� 50      $��30��6     ComboBoxBase#���  6'    �� 
IsEditable .   
ItemsStyle#���  6�   ��6N   �� *	ThemeList+ �� SelectedItem 6p   �� *SelectedTheme+6�   51      52      � 54   
   $��1��6   $��10q�6   .+�55      �� #���  6   
 56        57      $	 0.1Q�6'   $ 3Q�65   $ 2Q�6E   59      �� 5:      $��10q�6   .+�5;      �� $<��6Количество занятий умолчанию��6   $��14(�6@   $��Center=�6N   $
��10,5q�6k   #���  6y   �� 5<      $��1��6   
�������$��5,0,5,10q�66   �� 5=      $��30��6   #���  6'    . #���  6�   ��6N   �� *MumberLessonsList+ 6x   �� *SelectedNumberLessons+6�   5>      5?      � 5A   
   $��2��6   $��10q�6   .+�5B      �� #���  6   
 5C        5D      $	 0.1Q�6'   $ 3Q�65   $ 2Q�6E   5F      �� 5G      $��10q�6   .+�5H      �� $��Сервер��6   $��14(�6*   $��Center=�68   $
��10,5q�6U   #���  6c   �� 5I      $��1��6   
�������$��5,0,5,10q�66   �� 5J      
 � Tag$ Адрес сервера��61   $��30��6E     TextBoxBase#���  6Q   $��12(�6w   ��6   �� *Domen+6w   �� 5K      $! Пароль сервера��64   $��30��6I   #���  6U   $��12(�6{   $��	0,10,0,20q�6�   ��6   �� *
Password+6�   �� 5L      $��Сохранить��6   $��35��6-     
ButtonBase#���  69   ��6^   �� *SaveServerSettingsCommand+5M      5N      � 5P   
   $��3��6   $��10q�6   .+�5Q      �� #���  6   
 5R        5S      $	 0.1Q�6'   $ 3Q�65   $ 2Q�6E   5U      �� 5V      $��10q�6   .+�5W      �� $��Другое��6   $��14(�6*   $��Center=�68   $
��10,5q�6U   #���  6c   �� 5X      $��1��6   
�������$��5,0,5,10q�66   �� 5Y      $+��%Очистить расписание��6   $��35��67   #���  6C   ��6h   �� *DeleteAllScheduleCommand+�� 5Z      $=��7Удаляет расписание всех групп��6   $
��10,5q�6A   $��12(�6O     TextBlock.Static.SecondFG#���  6]   5[      5\      5^      5`      
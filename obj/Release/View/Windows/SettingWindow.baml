   M S B A M L   `   `   `  ���� P  LScheduleCollegeEditor, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ScheduleCollegeEditor.ViewScheduleCollegeEditor.View  Q-clr-namespace:ScheduleCollegeEditor.ViewModelScheduleCollegeEditor.ViewModel  .    (ScheduleCollegeEditor.View.SettingWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     :local(clr-namespace:ScheduleCollegeEditor.View     C	viewmodel-clr-namespace:ScheduleCollegeEditor.ViewModel     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  b ^GalaSoft.MvvmLight.Platform, Version=5.4.1.0, Culture=neutral, PublicKeyToken=5f873c45e98af8a1.command http://www.galasoft.ch/mvvmlight    � Title$  Настройки��$	��450��$	��610��$	��450��$	��610�� � 
ResizeMode$ CanMinimize=�   Window.Static.Background#���    � WindowStyle$ SingleBorderWindow=� � WindowStartupLocation$ CenterOwner=�.  (System.Windows.Interactivity.Interaction   Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ Loaded��.  (System.Windows.Interactivity.TriggerBase   Actions. /  )GalaSoft.MvvmLight.Command.EventToCommand     Command �� *LoadedAppCommand+ � DataContext <  6ScheduleCollegeEditor.ViewModel.SettingWindowViewModel  .��� 	 � RowDefinitions	 �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto �.+�� $��0��$��10,15,10,10q�.+���   
BorderBase#���  
 J� Effect
 3  -System.Windows.Media.Effects.DropShadowEffect     Opacity$	 0.1Q�   ShadowDepth$ 3Q�   
BlurRadius$ 2Q��� $��10q�.+��� $��Внешний вид��$��14(�$��Center=�$
��10,5q�  TextBlock.Static.Foreground#���  �� $��1��
�������$��5,0,5,10q��� $��30��  ComboBoxBase#���   �� 
IsEditable .   
ItemsStyle#���  ���� *	ThemeList+ �� SelectedItem �� *SelectedTheme+� $��1��$��10q�.+��� #���  
   $	 0.1Q�$ 3Q�$ 2Q��� $��10q�.+��� $A��;Количество занятий по умолчанию��$��14(�$��Center=�$
��10,5q�#���  �� $��1��
�������$��5,0,5,10q��� $��30��#���   . #���  ���� *MumberLessonsList+ �� *SelectedNumberLessons+� $��2��$��10q�.+��� #���  
   $	 0.1Q�$ 3Q�$ 2Q��� $��10q�.+��� $��Сервер��$��14(�$��Center=�$
��10,5q�#���  �� $��1��
�������$��5,0,5,10q��� 
 � Tag$ Адрес сервера��$��30��  TextBoxBase#���  $��12(����� *Domen+�� $! Пароль сервера��$��30��#���  $��12(�$��	0,10,0,20q����� *
Password+�� $��Сохранить��$��35��  
ButtonBase#���  ���� *SaveServerSettingsCommand+� $��3��$��10q�.+��� #���  
   $	 0.1Q�$ 3Q�$ 2Q��� $��10q�.+��� $��Другое��$��14(�$��Center=�$
��10,5q�#���  �� $��1��
�������$��5,0,5,10q��� $+��%Очистить расписание��$��35��#���  ���� *DeleteAllScheduleCommand+�� $=��7Удаляет расписание всех групп��$
��10,5q�$��12(�  TextBlock.Static.SecondFG#���  
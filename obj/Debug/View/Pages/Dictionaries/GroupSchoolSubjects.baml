   M S B A M L   `   `   `  ����P  LScheduleCollegeEditor, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ScheduleCollegeEditor.ViewScheduleCollegeEditor.View  5      Q-clr-namespace:ScheduleCollegeEditor.ViewModelScheduleCollegeEditor.ViewModel  5      4    .ScheduleCollegeEditor.View.GroupSchoolSubjects   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:ScheduleCollegeEditor.View     5      C	viewmodel-clr-namespace:ScheduleCollegeEditor.ViewModel     5      c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5      b ^GalaSoft.MvvmLight.Platform, Version=5.4.1.0, Culture=neutral, PublicKeyToken=5f873c45e98af8a1.command http://www.galasoft.ch/mvvmlight  5	      $��Stretch=�5      $��Stretch=�5      $	��600��5      $	��300��5      $��10q�5        � DataContext  5      B  <ScheduleCollegeEditor.ViewModel.GroupSchoolSubjectsViewModel  5   
   .��5      � $��Center=�6    � ColumnDefinitions 5   
   �� 5      $	��450��6   $	��350��6.   �� 5      $��40 �6   �� 5      $	��450��6   $	��350��6.    � RowDefinitions 5   
   �� 5      $��35 �6   �� 5      $��* �6   �� 5      $
��auto �6   .+�5"   
   �� 
 � Tag$ 
Поиск��6Q   $
��10,0q�6]      TextBoxBase#���   6k   ��6   �� *
FindText+ �� UpdateSourceTrigger$ PropertyChanged=�6k   �� 5$   
   $��2��6     ComboBoxBase#���  5'        
ItemsStyle#���  5(       �� 
IsEditable . $
��10,0q�5)   #   ��5%      �� *
GroupsList+ �� SelectedItem 5&      �� *SelectedGroup+5)   #   �� 5,   
   $��Stretch=�6   $��1��6.   $��10q�6;   $��15(�6G     ListBoxBase#���  6U   #���  5-   �   ��5,   {   �� *SchoolSubjectList+ 5-      �� *SelectedFromAllSchoolSubject+$ PropertyChanged=�{�6m   �� *ListboxEnable+.  (System.Windows.Interactivity.Interaction   Triggers 5.      /  )System.Windows.Interactivity.EventTrigger  5/         	EventName$ MouseDoubleClick��6!   .  (System.Windows.Interactivity.TriggerBase	   Actions.	 50      /  )GalaSoft.MvvmLight.Command.EventToCommand  
   Command
 6-   �� * AddSchoolSubjectInGroupCommand+51      53      �� 55   
   $��Stretch=�6   $��1��6.   $��2��6;   $��10q�6K   $��15(�6W   #���  6e   #���  56   �   ��55   �   �� *GroupSchoolSubjectList+ 56      �� *SelectedGroupSchoolSubject+$ PropertyChanged=�{�6k   �� *ListboxEnable+ 57        58      $ MouseDoubleClick��6!   .	 59        
 6-   �� *%#RemoveSchoolSubjectFromGroupCommand+5:      5<      �� 5>   
    �� Orientation$ Vertical=�6   $��1��6,   $��1��6<   $��35��6I   $��Center=�6T   .+�5?      ��   
ArrowRight#��[ 6   $��35(�6;   $��35��6I     
ButtonBase#���  6U   $	��0,5q�6z   ��6�   �� * AddSchoolSubjectInGroupCommand+�� 5@        	ArrowLeft#��[ 6   $��25(�6:   $��35��6H   #���  6T   $	��0,5q�6y   ��6�   �� *%#RemoveSchoolSubjectFromGroupCommand+�� 5A        DoubleArrowLeft#��[ 6   $��25(�6@   $��35��6N   #���  6Z   $	��0,5q�6   ��6�   �� *)'RemoveAllSchoolSubjectsFromGroupCommand+5B      5D      5E      
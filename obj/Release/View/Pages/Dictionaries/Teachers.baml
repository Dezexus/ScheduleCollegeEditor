   M S B A M L   `   `   `  ���� P  LScheduleCollegeEditor, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ScheduleCollegeEditor.ViewScheduleCollegeEditor.View  Q-clr-namespace:ScheduleCollegeEditor.ViewModelScheduleCollegeEditor.ViewModel  )    #ScheduleCollegeEditor.View.Teachers   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     :local(clr-namespace:ScheduleCollegeEditor.View     C	viewModel-clr-namespace:ScheduleCollegeEditor.ViewModel     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  b ^GalaSoft.MvvmLight.Platform, Version=5.4.1.0, Culture=neutral, PublicKeyToken=5f873c45e98af8a1.command http://www.galasoft.ch/mvvmlight  $��Stretch=�$��Stretch=�$	��600��$	��300��$��10q�  � DataContext  7  1ScheduleCollegeEditor.ViewModel.TeachersViewModel   � 	Resources    BoolToVisConverter��%   &	        �� .��� $��Center=� � RowDefinitions �� $��* ��� $��* ��� $��70 � � ColumnDefinitions �� $	��320��$	��350���� $	��450��$
��auto �.+��� $��Stretch=�$��2��$��10q�$��15(�  ListBoxBase#���    
ItemsStyle#���  ���� *TeachersList+ �� SelectedItem �� *SelectedTeacher+� $��2�� �� $��30 ��� $��10 ��� $��30 �.+��� $��Удалить��$	��140��$
��10,0q�$
��Left=�  
ButtonBase#���  ���� *DeleteTeacherCommand+���� *SelectedTeacher+�� $ ��Редактировать��$	��140��$
��10,0q�$��Right=�#���  ���� *EditTeacherCommand+���� *SelectedTeacher+�� $��2��$��Удалить все��$��30��$
��10,0q�#���  ���� *DeleteAllTeachersCommand+� $��35��$��1��$	��Top=�$��10q� �� $��* ��� $��35 �.+��� 
 � Tag$ 
Поиск��$	��5,0q�  TextBoxBase#���  ���� *
FindText+ �� UpdateSourceTrigger$ PropertyChanged=� � $��1��$��35��$��Right=�$	��Top=����� *LoadAnimation+y���  �� Path$ LoadAnimation� �� 	Converter# [  � $��1��$��Center=�$��Center=�$��10q�$��2�� �� $
��auto ��� $
��auto �.+��� $' !ФИО преподавателя��$	��350��$��35��$��1��$��10q�#���  ���� $ Teacher.FullName�$ PropertyChanged=�.��	 J� InputBindings	 �� 

 �� Key$
 Enter�� �� Command �� *AddTeacherCommand+ �� CommandParameter �� *	Teacher+.��� $��1��$��10, 0q�$��35��.+��� $��Отмена��$	��160��$
��Left=�#���  ���� *CancelEditTeacherCommand+���� *
EditMode+$ PropertyChanged=��� $	��160��$��Right=�#���  ���� *AddTeacherCommand+���� *	Teacher+���� *NameSaveButton+
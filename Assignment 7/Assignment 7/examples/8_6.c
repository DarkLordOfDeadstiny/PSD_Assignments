void main(int month)
{
	int days;
	switch (month) {
		case 1:
		{ days = 31; }
		case 2:
		{ days = 28; if (true) {days = 29;} }
		case 3:
		{ days = 31; }
	}
	print days;
}

#include <stdio.h>

int sample_function_001(int input1, int input2)
{
	int result_data = 0;
	
	if (input1 < input2) {
		result_data = input2 - input1;
	} else {
		result_data = input1 - input2;
	}
	
	return result_data;
}

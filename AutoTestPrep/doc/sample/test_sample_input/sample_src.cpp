#include <stdio.h>
#include <Windows.h>

int subFuncA_002(int input) {
	return 0;
};

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

int sample_function_002(int input1, int input2)
{
	int result_data = 0;
	
	if (input1 < input2) {
		result_data = subFuncA_002(input2);
	} else {
		result_data = subFuncA_002(input1);
	}
	
	return result_data;
}

int sample_function_003(int input1, int* input2, SHORT input3)
{
	int result_data = 0;
	
	if (input1 < input3) {
		*input2 = (input3 - input1) * 10;
		result_data = 1;
	} else {
		*input2 = (input1 - input3) * 10;
		result_data = 2;
	}
	
	return result_data;
}

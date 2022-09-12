#include <stdio.h>
#include <windows.h>
#include "gtest/gtest.h"
#include "UserHeader.h"
#include "sample_function_003_test.h"
#include "sample_function_003_stub.h"
//No global variables are refered by function sample_function_003.

//Test target function declare
int sample_function_003(int input1, int* input2, SHORT input3);

void sample_function_003_utest::SetUp()
{
}


TEST_F(sample_function_003_utest, sample_function_003_utest_1)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 1;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(1, _ret_val);
	ASSERT_EQ(20, input2);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_2)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 2;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(2, _ret_val);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_3)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 3;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(3, _ret_val);
	ASSERT_EQ(40, input2);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_4)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 0;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(0, _ret_val);
	ASSERT_EQ(10, input2);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_5)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 1;
	input2 = 0;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(1, _ret_val);
	ASSERT_EQ(20, input2);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_6)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 2;
	input2 = 0;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(2, _ret_val);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_7)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 3;
	input2 = 0;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(3, _ret_val);
	ASSERT_EQ(40, input2);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_8)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 1;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(1, _ret_val);
	ASSERT_EQ(20, input2);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_9)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 2;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(2, _ret_val);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_10)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 3;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(3, _ret_val);
	ASSERT_EQ(40, input2);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_11)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 0;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(0, _ret_val);
	ASSERT_EQ(10, input2);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_12)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 1;
	input2 = 0;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(1, _ret_val);
	ASSERT_EQ(20, input2);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_13)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 2;
	input2 = 0;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(2, _ret_val);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_14)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 3;
	input2 = 0;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(3, _ret_val);
	ASSERT_EQ(40, input2);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_15)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 1;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(1, _ret_val);
	ASSERT_EQ(20, input2);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_16)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 2;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(2, _ret_val);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_17)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 3;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(3, _ret_val);
	ASSERT_EQ(40, input2);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_18)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 0;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(0, _ret_val);
	ASSERT_EQ(10, input2);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_19)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 1;
	input2 = 0;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(1, _ret_val);
	ASSERT_EQ(20, input2);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_20)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 2;
	input2 = 0;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(2, _ret_val);
	ASSERT_EQ(30, input2);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_21)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 3;
	input2 = 0;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(3, _ret_val);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_22)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 1;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(1, _ret_val);
	ASSERT_EQ(20, input2);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_23)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 2;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(2, _ret_val);
	ASSERT_EQ(30, input2);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_24)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 3;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(3, _ret_val);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_25)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 0;
	input2 = 0;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(0, _ret_val);
	ASSERT_EQ(10, input2);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_26)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 1;
	input2 = 0;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(1, _ret_val);
	ASSERT_EQ(20, input2);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_27)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 2;
	input2 = 0;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(2, _ret_val);
	ASSERT_EQ(30, input2);
}

TEST_F(sample_function_003_utest, sample_function_003_utest_28)
{
	//Declare argument for target
	int input1;
	int input2;
	SHORT input3;

	//Setup test parameters.
	input1 = 3;
	input2 = 0;

	int _ret_val = sample_function_003(input1, &input2, input3);

	ASSERT_EQ(3, _ret_val);
}


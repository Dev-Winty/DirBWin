DirBuster 윈도우 버전
===========

### 1. 간단소개
- 웹서버의 디렉토리를 검색하기 위해 사용하는 dirb의 윈도우 버전

### 2. 기능

-------------------------

- #### 1. 디렉토리 검색 (유일한 기능)
	- 웹서버의 URL을 입력하고 Browse버튼을 통해 Wordlist를 선택 후 Run버튼을 통해 실행
	- 의심되는 디렉토리를 발견하면 HTTP 상태 코드와 디렉토리명을 ListView에 출력
	- 중지가능
  	

### 3. 사용 시 장점

----------------------

- #### 1. 속도가 빠르다
	- 한 요청을 처리한 이후 딜레이 없이 바로 요청을 보내기 때문에 매우 빠르다


### 4. 사용 시 단점
--------------

- #### 1. 속도가 너무 빠르다
	- 요청 간의 딜레이가 없기 때문에 IDS에 패킷 발생량 기반 탐지 룰이 설정돼 있을 경우 패킷이 Drop될 수 있다 <br/>에시: drop tcp any any -> any 80 (msg:"dirb"; threshold type both, track by_src, count 10, seconds 1; sid:100000001)
	
### 5. 스크린샷

<img src="https://user-images.githubusercontent.com/25945755/152683204-8486d59a-d007-4bbe-bdd1-946af9e108e1.png" width="40%" height="40%"/>

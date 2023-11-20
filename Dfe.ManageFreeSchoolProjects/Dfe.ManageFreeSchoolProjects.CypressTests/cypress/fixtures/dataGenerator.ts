class DataGenerator {
    generateTemporaryId():string {
        const values:string = 'abcdefghijklmnopqrstuvwxyz0123456789'
        let tempId:string = ''
        let temp:string = ''

        for (let i = 0; i < 25; i++) {
            temp = values.charAt(Math.round(values.length * Math.random()))
            tempId += temp
        }

        temp = ''

        return tempId;
    }

    generateAlphaNumeric(length: number):string {
        const values:string = 'abcdefghijklmnopqrstuvwxyz0123456789'
        let tempId:string = ''
        let temp:string = ''

        for (let i = 0; i < length; i++) {
            temp = values.charAt(i % values.length)
            tempId += temp
        }

        temp = ''

        return tempId;
    }
}

const dataGenerator = new DataGenerator();

export default dataGenerator;